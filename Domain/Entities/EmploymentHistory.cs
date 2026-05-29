using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;

namespace Domain.Entities
{
    public class EmploymentHistory
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public string OldDepartment { get; set; }

        public string NewDepartment { get; set; }

        public string OldPosition { get; set; }

        public string NewPosition { get; set; }

        public DateTime ChangeDate { get; set; }

        public EmploymentHistory()
        {
        }

        public EmploymentHistory(
            int employeeId,
            string oldDepartment,
            string newDepartment,
            string oldPosition,
            string newPosition)
        {
            EmployeeId = employeeId;
            OldDepartment = oldDepartment;
            NewDepartment = newDepartment;
            OldPosition = oldPosition;
            NewPosition = newPosition;
            ChangeDate = DateTime.Now;
        }
    }
}