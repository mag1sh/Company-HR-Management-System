using System;
using System.Collections.Generic;
using System.Linq; // Добавено за .ToList() метода
using System.Windows.Forms;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services.Interfaces;

namespace CompanyHRManagementSystem.Employees.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AddEmployee(Employee employee)
        {
            employee.Status = EmployeeStatus.Active;

            _repository.Save(employee);
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            var employee = GetById(updatedEmployee.Id);

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Address = updatedEmployee.Address;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.HireDate = updatedEmployee.HireDate;
            employee.DepartmentId = updatedEmployee.DepartmentId;
            employee.PositionId = updatedEmployee.PositionId;
            _repository.Save(employee);
        }

        public void DeactivateEmployee(int employeeId)
        {
            var employee = GetById(employeeId);
            employee.Status = EmployeeStatus.Inactive;
            employee.TerminationDate = DateTime.Now;

            _repository.Save(employee);
        }

        public List<Employee> GetAllActiveEmployees()
        {
            return _repository.GetAll().Where(e => e.Status == EmployeeStatus.Active).ToList();
        }

        public Employee GetById(int employeeId)
        {
            return _repository.GetById(employeeId);
        }

        public List<Employee> GetActiveEmployees()
        {
            return _repository.GetAll()
                .Where(e => e.Status == EmployeeStatus.Active)
                .ToList();
        }

        public void UpdateSalary(int employeeId, decimal newAmount)
        {
            _repository.UpdateSalary(employeeId, newAmount);
        }

        //public List<Employee> GenerateEmployeeTurnoverReport()
        //{
        //    //var report = _storage.Employees
        //    //    .GroupBy(e => e.HireDate.Year)
        //    //    .Select(g => new
        //    //    {
        //    //        Year = g.Key,
        //    //        HiredCount = g.Count(e => e.Status == EmployeeStatus.Active),
        //    //        LeftCount = g.Count(e => e.Status == EmployeeStatus.Inactive),
        //    //        TurnoverRate = g.Count(e => e.Status == EmployeeStatus.Active) == 0
        //    //            ? 0
        //    //            : (double)g.Count(e => e.Status == EmployeeStatus.Inactive) / g.Count(e => e.Status == EmployeeStatus.Active) * 100
        //    //    })
        //    //    .ToList();
        //    // return _repository.Employees.Where(e => e.Status == EmployeeStatus.Active).ToList(); //ignore
        //}

        //public List<Employee> GetEmployeeChangeHistory(int employeeId)
        //{

        //    //kak da go opraqv tva ve kak se pravi
        //    var employee = GetById(employeeId);
        //    var history = new List<Employee>();
        //    history.AddRange(_storage.EmploymentHistories.Where(h => h.EmployeeId == employeeId).Select(h =>
        //    {
        //        return new Employee
        //        {
        //            Id = employee.Id,
        //            Name = employee.Name,
        //            Email = employee.Email,
        //            Address = employee.Address,
        //            PhoneNumber = employee.PhoneNumber,
        //            HireDate = employee.HireDate,
        //            DepartmentId = h.NewDepartment != null ? int.Parse(h.NewDepartment) : employee.DepartmentId,
        //            PositionId = h.NewPosition != null ? int.Parse(h.NewPosition) : employee.PositionId,
        //            Status = employee.Status
        //        };
        //    }));
        //    history.AddRange(_storage.SalaryHistories.Where(s => s.EmployeeId == employeeId).Select(s =>
        //    {
        //        return new Employee
        //        {
        //            Id = employee.Id,
        //            Name = employee.Name,
        //            Email = employee.Email,
        //            Address = employee.Address,
        //            PhoneNumber = employee.PhoneNumber,
        //            HireDate = employee.HireDate,
        //            DepartmentId = employee.DepartmentId,
        //            PositionId = employee.PositionId,
        //            Status = employee.Status
        //        };
        //    }));
        //    return history.OrderByDescending(e => e.HireDate).ToList();
        //}

        //public List<Salary> GetSalaryHistory(int employeeId)
        //{
        //    return _storage.SalaryHistories.Where(s => s.EmployeeId == employeeId).ToList();
        //}
    }
}