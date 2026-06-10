using System;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Services;
using NUnit.Framework;

namespace CompanyHRManagementSystem.Tests.Leave_service
{
    [TestFixture]
    public class LeaveServiceTests
    {
        private FakeLeaveRepository _leaveRepository;
        private FakeEmployeeRepository _employeeRepository;
        private LeaveService _leaveService;

        [SetUp]
        public void SetUp()
        {
            _leaveRepository = new FakeLeaveRepository();
            _employeeRepository = new FakeEmployeeRepository();
            _leaveService = new LeaveService(_leaveRepository, _employeeRepository);
        }

        private Employee CreateTestEmployee(int id, int departmentId = 1)
        {
            var employee = new Employee(
                new FullName("Ivan", "Petrov"),
                new Email("ivan@test.com"),
                new PhoneNumber("0888111222"),
                new Address("Bulgaria", "Sofia", "1000", "Vitosha", "5"),
                DateTime.Now,
                departmentId,
                1
            );
            employee.Id = id;
            return employee;
        }

        private Leave CreateTestLeave(int employeeId, LeaveType type = LeaveType.Vacation,
            DateTime? start = null, DateTime? end = null)
        {
            var startDate = start ?? new DateTime(2026, 7, 1);
            var endDate = end ?? new DateTime(2026, 7, 5);
            return new Leave(employeeId, type, startDate, endDate);
        }

        [Test]
        public void RequestLeave_ShouldAddLeave()
        {
            var leave = CreateTestLeave(1);

            _leaveService.RequestLeave(leave);

            Assert.That(_leaveRepository.Leaves.Count, Is.EqualTo(1));
        }

        [Test]
        public void RequestLeave_ShouldThrowIfOverlappingApprovedLeave()
        {
            var existingLeave = CreateTestLeave(1);
            existingLeave.Approve();
            _leaveRepository.Leaves.Add(existingLeave);

            var newLeave = CreateTestLeave(1);

            Assert.Throws<Exception>(() => _leaveService.RequestLeave(newLeave));
        }

        [Test]
        public void RequestLeave_ShouldThrowIfExceedsMaxDays()
        {
            var leave = new Leave(1, LeaveType.Vacation, new DateTime(2026, 1, 1), new DateTime(2026, 1, 25));
            leave.Approve();
            _leaveRepository.Leaves.Add(leave);

            var newLeave = new Leave(1, LeaveType.Vacation, new DateTime(2026, 8, 1), new DateTime(2026, 8, 5));

            Assert.Throws<Exception>(() => _leaveService.RequestLeave(newLeave));
        }

        [Test]
        public void ApproveLeave_ShouldSetStatusToApproved()
        {
            var employee = CreateTestEmployee(1, 1);
            _employeeRepository.Employees.Add(employee);

            var leave = CreateTestLeave(1);
            leave.Id = 1;
            _leaveRepository.Leaves.Add(leave);

            _leaveService.ApproveLeave(1);

            Assert.That(leave.Status, Is.EqualTo(LeaveStatus.Approved));
        }

        [Test]
        public void ApproveLeave_ShouldThrowIfNotPending()
        {
            var employee = CreateTestEmployee(1, 1);
            _employeeRepository.Employees.Add(employee);

            var leave = CreateTestLeave(1);
            leave.Id = 1;
            leave.Approve();
            _leaveRepository.Leaves.Add(leave);

            Assert.Throws<Exception>(() => _leaveService.ApproveLeave(1));
        }

        [Test]
        public void RejectLeave_ShouldSetStatusToRejected()
        {
            var leave = CreateTestLeave(1);
            leave.Id = 1;
            _leaveRepository.Leaves.Add(leave);

            _leaveService.RejectLeave(1);

            Assert.That(leave.Status, Is.EqualTo(LeaveStatus.Rejected));
        }

        [Test]
        public void RejectLeave_ShouldThrowIfNotPending()
        {
            var leave = CreateTestLeave(1);
            leave.Id = 1;
            leave.Reject();
            _leaveRepository.Leaves.Add(leave);

            Assert.Throws<Exception>(() => _leaveService.RejectLeave(1));
        }

        [Test]
        public void GetUsedLeaveDays_ShouldReturnCorrectDays()
        {
            var leave = new Leave(1, LeaveType.Vacation, new DateTime(2026, 7, 1), new DateTime(2026, 7, 5));
            leave.Approve();
            _leaveRepository.Leaves.Add(leave);

            var result = _leaveService.GetUsedLeaveDays(1, 2026);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void GetRemainingLeaveDays_ShouldReturn20MinusUsed()
        {
            var leave = new Leave(1, LeaveType.Vacation, new DateTime(2026, 7, 1), new DateTime(2026, 7, 5));
            leave.Approve();
            _leaveRepository.Leaves.Add(leave);

            var result = _leaveService.GetRemainingLeaveDays(1, 2026);

            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void GetLeavesByPeriod_ShouldReturnCorrectLeaves()
        {
            var leave1 = CreateTestLeave(1, LeaveType.Vacation, new DateTime(2026, 7, 1), new DateTime(2026, 7, 5));
            var leave2 = CreateTestLeave(2, LeaveType.Vacation, new DateTime(2026, 8, 1), new DateTime(2026, 8, 5));
            _leaveRepository.Leaves.Add(leave1);
            _leaveRepository.Leaves.Add(leave2);

            var result = _leaveService.GetLeavesByPeriod(new DateTime(2026, 7, 1), new DateTime(2026, 7, 31));

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetVacationConflictsInDepartment_ShouldReturnConflicts()
        {
            var employee1 = CreateTestEmployee(1, 1);
            var employee2 = CreateTestEmployee(2, 1);
            _employeeRepository.Employees.Add(employee1);
            _employeeRepository.Employees.Add(employee2);

            var leave1 = CreateTestLeave(1);
            leave1.Approve();
            var leave2 = CreateTestLeave(2);
            leave2.Approve();
            _leaveRepository.Leaves.Add(leave1);
            _leaveRepository.Leaves.Add(leave2);

            var result = _leaveService.GetVacationConflictsInDepartment(1);

            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}