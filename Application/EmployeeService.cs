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
    public class EmployeeService
    {
        
            private readonly List<Employee> _employees = new List<Employee>();

            public void AddEmployee(Employee employee)
            {
                _employees.Add(employee);
            }

            public void UpdateEmployee(Employee updatedEmployee)
            {
                var employee = _employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);

                if (employee == null)
                    throw new Exception("Employee not found");

                employee.Name = updatedEmployee.Name;
                employee.Email = updatedEmployee.Email;
                employee.Address = updatedEmployee.Address;
                employee.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.HireDate = updatedEmployee.HireDate;
                employee.Status = updatedEmployee.Status;
                employee.TerminationDate = updatedEmployee.TerminationDate;
        }

            public void DeactivateEmployee(int employeeId)
            {
                var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

                if (employee == null)
                    throw new Exception("Employee not found");

                employee.Status = EmployeeStatus.Inactive;
            }

            public List<Employee> GetAllEmployees()
            {
                return _employees;
            }

            public Employee GetById(int employeeId)
            {
                var employee = _employees.FirstOrDefault(e => e.Id == employeeId);

                if (employee == null)
                    throw new Exception("Employee not found");

                return employee;
            }
        }
}

