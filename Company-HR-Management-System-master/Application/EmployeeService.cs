using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AddEmployee(Employee employee)
        {
            
            decimal initialSalaryAmount = employee.Salary != null ? employee.Salary.Amount : 1000;
         
            employee.Department = null;
            employee.Position = null;
            employee.Salary = null;

            employee.Status = EmployeeStatus.Active;

            _repository.Save(employee);
            _repository.UpdateSalary(employee.Id, initialSalaryAmount, "Начална заплата при назначаване");
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            _repository.Save(updatedEmployee);
        }

        public void DeactivateEmployee(int employeeId)
        {
            var employee = GetById(employeeId);
            employee.Status = EmployeeStatus.Inactive;
            employee.TerminationDate = DateTime.Now;

            _repository.Save(employee);
        }

        public List<Employee> GetAllEmployees()
        {
            return _repository.GetAll().ToList();
        }

        public List<Employee> GetAllActiveEmployees()
        {
            return _repository.GetAll().Where(e => e.Status == EmployeeStatus.Active).ToList();
        }

        public Employee GetById(int employeeId)
        {
            return _repository.GetById(employeeId);
        }

        public List<Employee> GetActiveEmployees()
        {
            return _repository.GetAll()
                .Where(e => e.Status == EmployeeStatus.Active)
                .ToList();
        }

        public void UpdateSalary(int employeeId, decimal newAmount, string reason)
        {
            _repository.UpdateSalary(employeeId, newAmount, reason);
        }

        public List<EmploymentHistory> GetEmploymentHistory(int employeeId)
        {
            return _repository.GetEmploymentHistory(employeeId);
        }

        public List<SalaryHistory> GetSalaryHistory(int employeeId)
        {
            return _repository.GetSalaryHistory(employeeId);
        }
    }
}