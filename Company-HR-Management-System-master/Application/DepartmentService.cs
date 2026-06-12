using System;
using System.Collections.Generic;
using System.Linq;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;

namespace CompanyHRManagementSystem.Application
{
    public class DepartmentService
    {

        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

       
        public void AddDepartment(Department department)
        {
            //if (string.IsNullOrWhiteSpace(department.Name))
            //    throw new Exception("Името на отдела не може да бъде празно!");

            if (_repository.ExistsByName(department.Name))
                throw new Exception($"Отдел с име '{department.Name}' вече съществува!");

            _repository.Save(department);
        }

        public List<Department> GetAllDepartments()
        {
            return _repository.GetAll().ToList();
        }

        public Department GetById(int departmentId)
        {
            return _repository.GetById(departmentId);
        }

        public void AssignEmployeeToDepartment(int employeeId, int departmentId, int positionId)
        {
            _repository.GetById(departmentId);
        }

       
        public bool HasLeaveConflictInDepartment(int departmentId, DateTime startDate, DateTime endDate)
        {
            return false;
        }
    }
}