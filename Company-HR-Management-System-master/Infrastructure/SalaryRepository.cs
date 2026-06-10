using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileSalaryRepository
    {
        public class SqlSalaryRepository
        {
            private readonly CompanyStorage _context;
           
            public SqlSalaryRepository(CompanyStorage context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
       
            public void Save(Salary salary)
            {
                
                _context.Salaries.Add(salary);

               
                _context.SaveChanges();
            }

            public IReadOnlyList<Salary> GetByEmployeeId(int employeeId)
            {
                List<Salary> employeeSalaries = new List<Salary>();

                foreach (var salary in _context.Salaries)
                {
                    if (salary.EmployeeId == employeeId)
                    {
                        employeeSalaries.Add(salary);
                    }
                }

                return employeeSalaries;
            }
        }
    }
}