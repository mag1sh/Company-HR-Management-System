using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileDepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyStorage _context;

        public FileDepartmentRepository(CompanyStorage context)
        {
            _context = context;
        }

        public void Save(Department department)
        {
            if (department == null)
                throw new ArgumentNullException("Отделът не може да е null!");
            
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public IReadOnlyList<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int departmentId)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentId == departmentId);

            if (department == null)
                throw new Exception("Отделът не е намерен!");

            return department;
        }

        public bool ExistsByName(string name)
        {
            return _context.Departments
                .Any(d => d.Name.ToLower() == name.ToLower());
        }
    }
}
