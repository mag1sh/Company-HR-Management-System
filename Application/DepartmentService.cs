using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Services;
using CompanyHRManagementSystem.Employees.Services.Interfaces;

namespace CompanyHRManagementSystem.Application
{
    public class DepartmentService
    {

        private readonly List<Department> _departments = new List<Department>();

        private readonly EmployeeService _employeeService;
        private readonly LeaveService _leaveService;


        public DepartmentService(EmployeeService employeeService, LeaveService leaveService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _leaveService = leaveService ?? throw new ArgumentNullException(nameof(leaveService));
        }


        public void AddDepartment(Department department)
        {
            if (string.IsNullOrWhiteSpace(department.Name))
                throw new Exception("Името на отдела не може да бъде празно!");

            
            foreach (var d in _departments)
            {
                if (d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Отдел с име '{department.Name}' вече съществува!");
                }
            }

            _departments.Add(department);
        }


        public List<Department> GetAllDepartments()
        {
            return _departments;
        }


        public void AssignEmployeeToDepartment(int employeeId, int departmentId, int positionId)
        {
            var employee = _employeeService.GetById(employeeId);

            
            Department foundDepartment = null;
            foreach (var d in _departments)
            {
                if (d.DepartmentId == departmentId) 
                {
                    foundDepartment = d;
                    break;
                }
            }

            if (foundDepartment == null)
            {
                throw new Exception("Избраният отдел не съществува!");
            }

            employee.DepartmentId = departmentId;
            employee.PositionId = positionId;
        }

        public Department GetById(int departmentId)
        {
            var department = _departments.FirstOrDefault(d => d.DepartmentId == departmentId);
            if (department == null)
                throw new Exception("Отделът не е намерен!");

            return department;
        }
    }
}
