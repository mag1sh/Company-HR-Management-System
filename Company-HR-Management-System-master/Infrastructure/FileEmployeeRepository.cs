using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using Domain.Entities;
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
                           .AsNoTracking()
                           .ToList();
        }

        public Employee GetById(int id)
        {

            var employee = _context.Employees
                                   .Include(e => e.Department)
                                   .Include(e => e.Position)
                                   .Include(e => e.Salary)
                                   .AsNoTracking()
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
                var existingEmployee = _context.Employees.Include(e => e.Salary).FirstOrDefault(e => e.Id == employee.Id);

                if (existingEmployee == null)
                {
                    throw new Exception("Employee not found for update");
                }

                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Address = employee.Address;
                existingEmployee.Status = employee.Status;
                existingEmployee.TerminationDate = employee.TerminationDate;
                existingEmployee.HireDate = employee.HireDate;


                if (existingEmployee.DepartmentId != employee.DepartmentId ||
                existingEmployee.PositionId != employee.PositionId)
                {
                    var history = new EmploymentHistory(employee.Id,
                                                        existingEmployee.DepartmentId.ToString(),
                                                        employee.DepartmentId.ToString(),
                                                        existingEmployee.PositionId.ToString(),
                                                        employee.PositionId.ToString()
                                                       );

                    _context.EmploymentHistories.Add(history);

                    existingEmployee.DepartmentId = employee.DepartmentId;
                    existingEmployee.PositionId = employee.PositionId;

                    var newPosition = _context.Positions
                        .FirstOrDefault(p => p.Id == employee.PositionId);

                    if (newPosition != null && existingEmployee.Salary != null)
                    {
                        existingEmployee.Salary.Amount = newPosition.BaseSalary;
                    }
                }   
                _context.SaveChanges();
                return;
            }
            _context.SaveChanges();
        }

        public void UpdateSalary(int employeeId, decimal newAmount, string reason)
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
                var salaryHistory = new SalaryHistory(employeeId,
                                                      salary.Amount,
                                                      newAmount,
                                                      reason
                                                      );
                _context.SalaryHistories.Add(salaryHistory);

                salary.Amount = newAmount;
            }
            _context.SaveChanges();
        }

        public List<EmploymentHistory> GetEmploymentHistory(int employeeId)
        {
            return _context.EmploymentHistories
                .Where(h => h.EmployeeId == employeeId)
                .OrderByDescending(h => h.ChangeDate)
                .ToList();
        }

        public List<SalaryHistory> GetSalaryHistory(int employeeId)
        {
            return _context.SalaryHistories
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.ChangeDate)
                .ToList();
        }
    }
}