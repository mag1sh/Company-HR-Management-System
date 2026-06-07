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
        IReadOnlyList<Department> GetByDepartmentId(int departmentId);
       //ReadOnlyList<Department> GetByDepartmentName(string departmentName);
    }
}
