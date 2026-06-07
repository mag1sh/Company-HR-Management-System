using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        void DeactivateEmployee(int employeeId);

        List<Employee> GetAllEmployees();

        Employee GetById(int employeeId);
    }
}