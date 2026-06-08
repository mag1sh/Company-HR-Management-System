using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class LeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private const int MaxPaidLeaveDaysPerYear = 20;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRepository = leaveRepository ?? throw new ArgumentNullException(nameof(leaveRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public void RequestLeave(Leave leave)
        {
           
            foreach (var l in _leaveRepository.GetAll())
            {
                if (l.EmployeeId == leave.EmployeeId && l.Status == LeaveStatus.Approved)
                {
                    if (leave.StartDate <= l.EndDate && leave.EndDate >= l.StartDate)
                    {
                        throw new Exception("Служителят вече има одобрен отпуск или болничен за този период!");
                    }
                }
            }

           
            if (leave.LeaveType == LeaveType.Vacation) 
            {
                int usedDays = GetUsedLeaveDays(leave.EmployeeId, leave.StartDate.Year);

                if (usedDays + leave.DaysCount > MaxPaidLeaveDaysPerYear)
                {
                    throw new Exception($"Нямате достатъчно дни! Остават ви: {MaxPaidLeaveDaysPerYear - usedDays} дни за тази година.");
                }
            }
            _leaveRepository.Save(leave);
        }

        public Leave GetLeaveById(int leaveId)
        {
            return _leaveRepository.GetById(leaveId);
        }

        public void ApproveLeave(int leaveId)
        {
            var leave = _leaveRepository.GetById(leaveId);
            if (leave.Status != LeaveStatus.Pending)
                throw new Exception("Могат да се одобряват само чакащи (Pending) заявки!");

            var employee = _employeeRepository.GetById(leave.EmployeeId);

            if (HasConflictInDepartment(leave, employee.DepartmentId))
            {
                _leaveRepository.Delete(leaveId);
                throw new Exception("Приемането на заявката е неуспешно поради създаване на конфликт. Заявката е изтрита автоматично.");
            }

            leave.Approve();
            _leaveRepository.SaveChanges();

            //var leave = _leaveRepository.GetById(leaveId);
            //if (leave.Status != LeaveStatus.Pending)
            //    throw new Exception("Могат да се одобряват само чакащи (Pending) заявки!");

            //foreach (var l in _leaveRepository.GetAll())
            //{
            //    if (l.EmployeeId == leave.EmployeeId && l.Status == LeaveStatus.Approved && l.Id != leaveId)
            //    {
            //        if (leave.StartDate <= l.EndDate && leave.EndDate >= l.StartDate)
            //            throw new Exception("Служителят вече има одобрен отпуск за този период!");
            //    }
            //}
            //leave.Approve();
            //_leaveRepository.SaveChanges();
        }

        
        public void RejectLeave(int leaveId)
        {
            var leave = _leaveRepository.GetById(leaveId);
            if (leave.Status != LeaveStatus.Pending)
                throw new Exception("Могат да се отказват само чакащи (Pending) заявки!");
            leave.Reject();
            _leaveRepository.SaveChanges();
        }

        
        public int GetUsedLeaveDays(int employeeId, int year)
        {
            int totalDays = 0;
            foreach (var l in _leaveRepository.GetAll())
            {
                if (l.EmployeeId == employeeId &&
                    l.Status == LeaveStatus.Approved &&
                    l.LeaveType == LeaveType.Vacation &&
                    l.StartDate.Year == year)
                {
                    totalDays += l.DaysCount;
                }
            }
            return totalDays;
        }

       
        public int GetRemainingLeaveDays(int employeeId, int year)
        {
            return MaxPaidLeaveDaysPerYear - GetUsedLeaveDays(employeeId, year);
        }

        public List<Leave> GetAllLeaves()
        {
            return _leaveRepository.GetAll().ToList();
        }

        public List<(Leave Leave1, Leave Leave2)> GetVacationConflictsInDepartment(int departmentId)
        {
            var employeeIds = _employeeRepository.GetAll()
                .Where(e => e.DepartmentId == departmentId)
                .Select(e => e.Id)
                .ToHashSet();

            var approvedLeaves = _leaveRepository.GetAll()
                .Where(l => l.Status == LeaveStatus.Approved && employeeIds.Contains(l.EmployeeId))
                .ToList();

            var conflicts = new List<(Leave Leave1, Leave Leave2)>();
            for (int i = 0; i < approvedLeaves.Count; i++)
            {
                for (int j = i + 1; j < approvedLeaves.Count; j++)
                {
                    if (approvedLeaves[i].StartDate <= approvedLeaves[j].EndDate &&
                        approvedLeaves[i].EndDate >= approvedLeaves[j].StartDate)
                    {
                        conflicts.Add((approvedLeaves[i], approvedLeaves[j]));
                    }
                }
            }
            return conflicts;
        }

        public List<Leave> GetLeavesByPeriod(DateTime startDate, DateTime endDate)
        {
            return _leaveRepository.GetAll()
               .Where(l => l.StartDate <= endDate && l.EndDate >= startDate)
               .ToList();
        }

        public bool HasConflictInDepartment(Leave leave, int departmentId)
        {
            var employeeIds = _employeeRepository.GetAll()
                .Where(e => e.DepartmentId == departmentId && e.Id != leave.EmployeeId)
                .Select(e => e.Id)
                .ToList();

            foreach (var l in _leaveRepository.GetAll())
            {
                if (employeeIds.Contains(l.EmployeeId) && l.Status == LeaveStatus.Approved && l.Id != leave.Id)
                {
                    if (leave.StartDate <= l.EndDate && leave.EndDate >= l.StartDate)
                        return true;
                }
            }
            return false;
        }
    }
}