using CompanyHRManagementSystem.Employees.Domain.Enums;
using System;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; private set; }
        public  Employee Employee { get; private set; }
        public LeaveType LeaveType { get;  set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int DaysCount { get; set; }
        public LeaveStatus Status { get;  set; }

        protected Leave() { }

        public Leave(int employeeId, LeaveType leaveType, DateTime startDate, DateTime endDate)
        {
            if(endDate < startDate)
                throw new ArgumentException("End date cannot be before start date.");
            EmployeeId = employeeId;
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;

            DaysCount = (endDate - startDate).Days + 1;
            Status = LeaveStatus.Pending;
        }

        public void Approve()
        {
            Status = LeaveStatus.Approved;
        }

        public void Reject()
        {
            Status = LeaveStatus.Rejected;
        }

        public void Cancel()
        {
            Status = LeaveStatus.Cancelled;
        }
    }
}