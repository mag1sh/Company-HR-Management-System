using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
