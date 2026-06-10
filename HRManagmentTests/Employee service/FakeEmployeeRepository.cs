using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using Domain.Entities;
using System.Linq;

namespace CompanyHRManagementSystem.Tests
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public List<Employee> Employees = new List<Employee>();

        public Employee GetById(int id)
        {
            var employee = Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new Exception("Employee not found");
            return employee;
        }

        public IReadOnlyList<Employee> GetAll()
        {
            return Employees;
        }

        public void Save(Employee employee)
        {
            if (employee.Id == 0)
            {
                employee.Id = Employees.Count + 1;
                Employees.Add(employee);
            }
            else
            {
                var existing = Employees.FirstOrDefault(e => e.Id == employee.Id);
                if (existing != null)
                {
                    existing.Name = employee.Name;
                    existing.Status = employee.Status;
                    existing.TerminationDate = employee.TerminationDate;
                    existing.DepartmentId = employee.DepartmentId;
                    existing.PositionId = employee.PositionId;
                }
            }
        }

        public void UpdateSalary(int employeeId, decimal newAmount, string reason)
        {
        }

        public List<EmploymentHistory> GetEmploymentHistory(int employeeId)
        {
            return new List<EmploymentHistory>();
        }

        public List<SalaryHistory> GetSalaryHistory(int employeeId)
        {
            return new List<SalaryHistory>();
        }
    }
}