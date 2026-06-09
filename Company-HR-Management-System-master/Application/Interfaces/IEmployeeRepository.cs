using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using Domain.Entities;

namespace CompanyHRManagementSystem.Employees.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);

        IReadOnlyList<Employee> GetAll();

        void Save(Employee employee);

        void UpdateSalary(int employeeId, decimal newAmount, string reason);

        List<EmploymentHistory> GetEmploymentHistory(int employeeId);

        List<SalaryHistory> GetSalaryHistory(int employeeId);
    }
}
