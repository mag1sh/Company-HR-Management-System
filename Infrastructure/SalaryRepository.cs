using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileSalaryRepository 
    {
        private readonly FileStorage _storage;

        public FileSalaryRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public void Save(Salary salary)
        {
            var db = _storage.Load();

            salary.Id = db.NextId++;
            db.Salaries.Add(salary);

            _storage.Save(db);
        }

        public IReadOnlyList<Salary> GetByEmployeeId(int employeeId)
        {
            var db = _storage.Load();

            return db.Salaries
                .Where(s => s.EmployeeId == employeeId)
                .ToList();
        }
    }
}