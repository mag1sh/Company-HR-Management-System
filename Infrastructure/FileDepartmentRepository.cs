using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileDepartmentRepository : IDepartmentRepository
    {
        private readonly FileStorage _storage;

        public FileDepartmentRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public void Save(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);

            var db = _storage.Load();
            var newDepartment = new Department(
                db.NextId++,
                department.Name,
                department.Description
                );

            db.Departments.Add(newDepartment);

            _storage.Save(db);
        }

        public IReadOnlyList<Department> GetByDepartmentId(int departmentId)
        {
            var db = _storage.Load();

            List<Department> result = new List<Department>();

            foreach (Department department in db.Departments)
            {
                if (department.DepartmentId == departmentId)
                {
                    result.Add(department);
                }
            }
            return result;
        }
    }
}
