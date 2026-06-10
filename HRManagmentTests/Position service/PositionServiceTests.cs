using System;
using CompanyHRManagementSystem.Application;
using Domain.Entities;
using NUnit.Framework;

namespace CompanyHRManagementSystem.Tests.Position_service
{
    [TestFixture]
    public class PositionServiceTests
    {
        private FakePositionRepository _repository;
        private PositionService _positionService;

        [SetUp]
        public void SetUp()
        {
            _repository = new FakePositionRepository();
            _positionService = new PositionService(_repository);
        }

        [Test]
        public void AddPosition_ShouldAddToRepository()
        {
            var position = new Position("Developer", "описание", 1200, 1);

            _positionService.AddPosition(position);

            Assert.That(_repository.Positions.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddPosition_ShouldThrowIfTitleIsEmpty()
        {
            var position = new Position("", "описание", 1200, 1);

            Assert.Throws<Exception>(() => _positionService.AddPosition(position));
        }

        [Test]
        public void AddPosition_ShouldThrowIfDuplicateTitle()
        {
            var position1 = new Position("Developer", "описание", 1200, 1);
            var position2 = new Position("Developer", "описание2", 1500, 1);
            _positionService.AddPosition(position1);

            Assert.Throws<Exception>(() => _positionService.AddPosition(position2));
        }

        [Test]
        public void GetAllPositions_ShouldReturnAll()
        {
            _repository.Positions.Add(new Position("Developer", "описание", 1200, 1));
            _repository.Positions.Add(new Position("Manager", "описание", 2000, 1));

            var result = _positionService.GetAllPositions();

            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetById_ShouldThrowIfNotFound()
        {
            Assert.Throws<Exception>(() => _positionService.GetById(99));
        }

        [Test]
        public void GetByDepartmentId_ShouldReturnOnlyFromDepartment()
        {
            _repository.Positions.Add(new Position("Developer", "описание", 1200, 1) { Id = 1 });
            _repository.Positions.Add(new Position("Manager", "описание", 2000, 2) { Id = 2 });

            var result = _positionService.GetByDepartmentId(1);

            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}