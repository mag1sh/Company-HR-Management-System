using System;
using System.Collections.Generic;
using System.Linq; // Добавено за .ToList() метода
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Infrastructure;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class EmployeeService
    {
        private readonly CompanyStorage _storage;

        public EmployeeService(CompanyStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void AddEmployee(Employee employee)
        {
            employee.Status = EmployeeStatus.Active;

            _storage.Employees.Add(employee);

            _storage.SaveChanges();
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            var employee = GetById(updatedEmployee.Id);

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Address = updatedEmployee.Address;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.HireDate = updatedEmployee.HireDate;

            _storage.SaveChanges();
        }

        public void DeactivateEmployee(int employeeId)
        {
            var employee = GetById(employeeId);
            employee.Status = EmployeeStatus.Inactive;
            employee.TerminationDate = DateTime.Now;

            _storage.SaveChanges();
        }

        public List<Employee> GetAllEmployees()
        {
            return _storage.Employees.ToList();
        }

        public Employee GetById(int employeeId)
        {
            foreach (var e in _storage.Employees)
            {
                if (e.Id == employeeId)
                    return e;
            }
            throw new Exception("Служителят не е намерен!");
        }

        public List<Employee> GetActiveEmployees()
        {
            List<Employee> active = new List<Employee>();
            foreach (var e in _storage.Employees)
            {
                if (e.Status == EmployeeStatus.Active)
                    active.Add(e);
            }
            return active;
        }
    }
}