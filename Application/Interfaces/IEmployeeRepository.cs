using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;

namespace CompanyHRManagementSystem.Employees.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);

        IReadOnlyList<Employee> GetAll();

        void Save(Employee employee);
    }
}
