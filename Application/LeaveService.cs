using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Infrastructure;
using Services.Interfaces;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class LeaveService
    {
        private readonly CompanyStorage _storage;
        private const int MaxPaidLeaveDaysPerYear = 20;

        public LeaveService(CompanyStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

       
        public void RequestLeave(Leave leave)
        {
           
            foreach (var l in _storage.Leaves)
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

            _storage.Leaves.Add(leave);
            _storage.SaveChanges();

        }

       
        public void ApproveLeave(int leaveId)
        {
            foreach (var l in _storage.Leaves)
            {
                if (l.Id == leaveId)
                {
                    if (l.Status != LeaveStatus.Pending)
                        throw new Exception("Могат да се одобряват само чакащи (Pending) заявки!");

                    l.Approve();
                    return;
                }
            }
            throw new Exception("Заявката за отпуск не е намерена!");
        }

        
        public void RejectLeave(int leaveId)
        {
            foreach (var l in _storage.Leaves)
            {
                if (l.Id == leaveId)
                {
                    if (l.Status != LeaveStatus.Pending)
                        throw new Exception("Могат да се отказват само чакащи (Pending) заявки!");

                    l.Reject();
                    _storage.SaveChanges();
                    return;
                }
            }
            throw new Exception("Заявката за отпуск не е намерена!");
        }

        
        public int GetUsedLeaveDays(int employeeId, int year)
        {
            int totalDays = 0;
            foreach (var l in _storage.Leaves)
            {
                if (l.EmployeeId == employeeId && l.Status == LeaveStatus.Approved && l.LeaveType == LeaveType.Vacation && l.StartDate.Year == year)
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
            return _storage.Leaves.ToList();
        }
    }

}

