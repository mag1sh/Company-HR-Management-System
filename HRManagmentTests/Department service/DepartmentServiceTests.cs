using System;
using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace CompanyHRManagementSystem.Tests.Department_service
{
    [TestFixture]
    public class DepartmentServiceTests
    {
        private FakeDepartmentRepository _repository;
        private DepartmentService _departmentService;

        [SetUp]
        public void SetUp()
        {
            _repository = new FakeDepartmentRepository();
            _departmentService = new DepartmentService(_repository);
        }

        [Test]
        public void AddDepartment_ShouldAddToRepository()
        {
            var department = new Department("HR", "описание");

            _departmentService.AddDepartment(department);

            Assert.That(_repository.Departments.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddDepartment_ShouldThrowIfNameIsEmpty()
        {
            var department = new Department("", "описание");

            Assert.Throws<Exception>(() => _departmentService.AddDepartment(department));
        }

        [Test]
        public void AddDepartment_ShouldThrowIfDuplicateName()
        {
            var department1 = new Department("HR", "описание");
            var department2 = new Department("HR", "описание2");
            _departmentService.AddDepartment(department1);

            Assert.Throws<Exception>(() => _departmentService.AddDepartment(department2));
        }

        [Test]
        public void GetAllDepartments_ShouldReturnAll()
        {
            _repository.Departments.Add(new Department("HR", "описание"));
            _repository.Departments.Add(new Department("IT", "описание"));

            var result = _departmentService.GetAllDepartments();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetById_ShouldThrowIfNotFound()
        {
            Assert.Throws<Exception>(() => _departmentService.GetById(99));
        }
    }
}