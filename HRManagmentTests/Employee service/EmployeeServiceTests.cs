using System;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Services;
using CompanyHRManagementSystem.Infrastructure;
using NUnit.Framework;

namespace CompanyHRManagementSystem.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        private FakeEmployeeRepository _repository;
        private EmployeeService _employeeService;

        [SetUp]
        public void SetUp()
        {
            _repository = new FakeEmployeeRepository();
            _employeeService = new EmployeeService(_repository);
        }

        private Employee CreateTestEmployee(int id = 0, EmployeeStatus status = EmployeeStatus.Active)
        {
            var employee = new Employee(
                new FullName("Ivan", "Petrov"),
                new Email("ivan@test.com"),
                new PhoneNumber("0888111222"),
                new Address("Bulgaria", "Sofia", "1000", "Vitosha", "5"),
                DateTime.Now,
                1,
                1
            );
            employee.Id = id;
            employee.Status = status;
            return employee;
        }

        [Test]
        public void AddEmployee_ShouldSetStatusToActive()
        {
            var employee = CreateTestEmployee();
            employee.Status = EmployeeStatus.Inactive;

            _employeeService.AddEmployee(employee);

            Assert.That(employee.Status, Is.EqualTo(EmployeeStatus.Active));
        }

        [Test]
        public void AddEmployee_ShouldAddToRepository()
        {
            var employee = CreateTestEmployee();

            _employeeService.AddEmployee(employee);

            Assert.That(_repository.Employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetById_ShouldReturnCorrectEmployee()
        {
            var employee = CreateTestEmployee(1);
            _repository.Employees.Add(employee);

            var result = _employeeService.GetById(1);

            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetById_ShouldThrowIfNotFound()
        {
            Assert.Throws<Exception>(() => _employeeService.GetById(99));
        }

        [Test]
        public void GetAllActiveEmployees_ShouldReturnOnlyActive()
        {
            _repository.Employees.Add(CreateTestEmployee(1, EmployeeStatus.Active));
            _repository.Employees.Add(CreateTestEmployee(2, EmployeeStatus.Inactive));
            _repository.Employees.Add(CreateTestEmployee(3, EmployeeStatus.Active));

            var result = _employeeService.GetAllActiveEmployees();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void DeactivateEmployee_ShouldSetStatusToInactive()
        {
            var employee = CreateTestEmployee(1, EmployeeStatus.Active);
            _repository.Employees.Add(employee);

            _employeeService.DeactivateEmployee(1);

            Assert.That(employee.Status, Is.EqualTo(EmployeeStatus.Inactive));
        }

        [Test]
        public void DeactivateEmployee_ShouldSetTerminationDate()
        {
            var employee = CreateTestEmployee(1, EmployeeStatus.Active);
            _repository.Employees.Add(employee);

            _employeeService.DeactivateEmployee(1);

            Assert.That(employee.TerminationDate, Is.Not.Null);
        }

        [Test]
        public void UpdateEmployee_ShouldUpdateDepartment()
        {
            var employee = CreateTestEmployee(1);
            _repository.Employees.Add(employee);
            employee.DepartmentId = 2;

            _employeeService.UpdateEmployee(employee);

            Assert.That(_repository.Employees[0].DepartmentId, Is.EqualTo(2));
        }
    }
}