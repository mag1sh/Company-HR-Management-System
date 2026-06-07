using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;

namespace Domain.Entities
{
    public class SalaryHistory
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public  Employee Employee { get; set; }


        public decimal OldSalary { get; set; }

        public decimal NewSalary { get; set; }

        public DateTime ChangeDate { get; set; }

        public string Reason { get; set; }

        public SalaryHistory()
        {
        }

        public SalaryHistory(int employeeId, decimal oldSalary, decimal newSalary, string reason)
        {
            EmployeeId = employeeId;
            OldSalary = oldSalary;
            NewSalary = newSalary;
            Reason = reason;
            ChangeDate = DateTime.Now;
        }
    }
}