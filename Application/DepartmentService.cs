using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services;
using CompanyHRManagementSystem.Employees.Services.Interfaces;

namespace CompanyHRManagementSystem.Application
{
    public class DepartmentService
    {

        private readonly CompanyStorage _storage;

        public DepartmentService(CompanyStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

       
        public void AddDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                throw new Exception("Името на отдела не може да бъде празно!");

            
            foreach (var d in _storage.Departments)
            {
                if (d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase))
                    throw new Exception($"Отдел с име '{department.Name}' вече съществува!");
            }

            _storage.Departments.Add(department);
        }

        public List<Department> GetAllDepartments()
        {
            return _storage.Departments.ToList();
        }

       
        public void AssignEmployeeToDepartment(int employeeId, int departmentId, int positionId)
        {
           
            Employee employee = null;
            foreach (var e in _storage.Employees)
            {
                if (e.Id == employeeId) { employee = e; break; }
            }
            if (employee == null) throw new Exception("Служителят не е намерен!");

           
            Department department = null;
            foreach (var d in _storage.Departments)
            {
                if (d.DepartmentId == departmentId) { department = d; break; } 
            }
            if (department == null) throw new Exception("Избраният отдел не съществува!");

            
            employee.DepartmentId = departmentId;
            employee.PositionId = positionId;
        }

       
        public bool HasLeaveConflictInDepartment(int departmentId, DateTime startDate, DateTime endDate)
        {
            
            List<int> employeeIdsInDept = new List<int>();
            foreach (var e in _storage.Employees)
            {
                if (e.DepartmentId == departmentId && e.Status == EmployeeStatus.Active)
                    employeeIdsInDept.Add(e.Id);
            }

          
            foreach (var leave in _storage.Leaves)
            {
                if (leave.Status == LeaveStatus.Approved && employeeIdsInDept.Contains(leave.EmployeeId))
                {
                  
                    if (startDate <= leave.EndDate && endDate >= leave.StartDate)
                    {
                        return true; 
                    }
                }
            }
            return false;
        }

        public Department GetById(int departmentId)
        {
            foreach (var d in _storage.Departments)
            {
                if (d.DepartmentId == departmentId) return d;
            }
            throw new Exception("Отделът не е намерен!");
        }
    }
}
