using CompanyHRManagementSystem.Employees.Domain.Entities;
using System.Collections.Generic;

namespace CompanyHRManagementSystem.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        void Save(Department department);

        IReadOnlyList<Department> GetAll();

        Department GetById(int departmentId);

        bool ExistsByName(string name);
    }
}
