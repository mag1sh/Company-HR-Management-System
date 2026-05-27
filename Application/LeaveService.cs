using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using Services.Interfaces;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly List<Leave> _leaves = new List<Leave>();

        public void RequestLeave(Leave leave)
        {
            bool hasConflict = _leaves.Any(l =>
                l.EmployeeId == leave.EmployeeId &&
                l.Status == LeaveStatus.Approved &&
                leave.StartDate <= l.EndDate &&
                leave.EndDate >= l.StartDate);

            if (hasConflict)
                throw new Exception("Leave conflict detected");


            _leaves.Add(leave);
        }

        public void ApproveLeave(int leaveId)
        {
            var leave = _leaves.FirstOrDefault(l => l.Id == leaveId);

            if (leave == null)
                throw new Exception("Leave not found");

            leave.Approve();
        }



        public void RejectLeave(int leaveId)
        {
            var leave = _leaves.FirstOrDefault(l => l.Id == leaveId);

            if (leave == null)
                throw new Exception("Leave not found");

            leave.Reject();
        }

        public int GetUsedLeaveDays(int employeeId, int year)
        {
            return _leaves
                .Where(l => l.EmployeeId == employeeId &&
                            l.Status == LeaveStatus.Approved &&
                            l.LeaveType == LeaveType.Vacation &&
                            l.StartDate.Year == year)
                .Sum(l => (l.EndDate - l.StartDate).Days + 1);
        }


        public List<Leave> GetLeavesByPeriod(DateTime start, DateTime end)
        {
            return _leaves.Where(l => l.StartDate >= start && l.EndDate <= end).ToList();
        }

        public List<Leave> GetAllLeaves()
        {
            return _leaves;
        }
    }
}

