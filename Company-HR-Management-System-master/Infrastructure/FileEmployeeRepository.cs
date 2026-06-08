using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Infrastructure
{
    public class FileEmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyStorage _context;


        public FileEmployeeRepository(CompanyStorage context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IReadOnlyList<Employee> GetAll()
        {
            return _context.Employees
                           .Include(e => e.Department)
                           .Include(e => e.Position)
                           .Include(e => e.Salary)
                           .ToList();
        }


        public Employee GetById(int id)
        {

            var employee = _context.Employees
                                   .Include(e => e.Department)
                                   .Include(e => e.Position)
                                   .Include(e => e.Salary)
                                   .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                throw new Exception("Employee not found in database");

            return employee;
        }


        public void Save(Employee employee)
        {
            if (employee.Id == 0)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                var position = _context.Positions.FirstOrDefault(p => p.Id == employee.PositionId);
                if (position == null)
                    throw new Exception("Позицията не е намерена!");

                var salary = new Salary
                {
                    EmployeeId = employee.Id,
                    Amount = position.BaseSalary
                };

                _context.Salaries.Add(salary);
                _context.SaveChanges();
            }
            else
            {
                Employee existingEmployee = null;
                foreach (var e in _context.Employees)
                {
                    if (e.Id == employee.Id)
                    {
                        existingEmployee = e;
                        break;
                    }
                }

                if (existingEmployee == null)
                {
                    throw new Exception("Employee not found for update");
                }

                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Address = employee.Address;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.PositionId = employee.PositionId;
                existingEmployee.Status = employee.Status;
                existingEmployee.TerminationDate = employee.TerminationDate;
                existingEmployee.HireDate = employee.HireDate;


                if (existingEmployee.DepartmentId != employee.DepartmentId ||
                existingEmployee.PositionId != employee.PositionId)
                {
                    existingEmployee.DepartmentId = employee.DepartmentId;
                    existingEmployee.PositionId = employee.PositionId;

                    var newPosition = _context.Positions
                        .FirstOrDefault(p => p.Id == employee.PositionId);

                    if (newPosition != null && existingEmployee.Salary != null)
                    {
                        existingEmployee.Salary.Amount = newPosition.BaseSalary;
                    }
                }
            }

            _context.SaveChanges();
        }

        public void UpdateSalary(int employeeId, decimal newAmount)
        {
            var salary = _context.Salaries.FirstOrDefault(s => s.EmployeeId == employeeId);
            if (salary == null)
            {
                _context.Salaries.Add(new Salary
                {
                    EmployeeId = employeeId,
                    Amount = newAmount
                });
            }
            else
            {
                salary.Amount = newAmount;
            }
            _context.SaveChanges();
        }
    }
}
