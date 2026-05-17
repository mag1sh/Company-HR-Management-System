using System.Collections.Generic;
using CompanyHRManagementSystem.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Entities;

namespace CompanyHRManagementSystem.Employees.Services.Interfaces
{
    internal interface ILeaveRepository
    {
        Leave GetById(int id);

        IReadOnlyList<Leave> GetAll();

        void Save(Leave leave);

        void Delete(int id);

        IReadOnlyList<Leave> GetByEmployeeId(int employeeId);

        IReadOnlyList<Leave> GetByDepartmentId(int departmentId);

        
    }
}