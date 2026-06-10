using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System.Linq;
namespace CompanyHRManagementSystem.Tests.Department_service
{
    public class FakeDepartmentRepository : IDepartmentRepository
    {
        public List<Department> Departments = new List<Department>();

        public void Save(Department department)
        {
            Departments.Add(department);
        }

        public IReadOnlyList<Department> GetAll()
        {
            return Departments;
        }

        public Department GetById(int departmentId)
        {
            var department = Departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department == null)
                throw new Exception("Отделът не е намерен!");
            return department;
        }

        public bool ExistsByName(string name)
        {
            return Departments.Any(d => d.Name.ToLower() == name.ToLower());
        }
    }
}