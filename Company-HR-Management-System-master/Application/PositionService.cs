using System;
using System.Collections.Generic;
using System.Linq;
using CompanyHRManagementSystem.Application.Interfaces;
using Domain.Entities;

namespace CompanyHRManagementSystem.Application
{
    public class PositionService
    {


        private readonly IPositionRepository _repository;


        public PositionService(IPositionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AddPosition(Position position)
        {
            if (string.IsNullOrWhiteSpace(position.Title))
                throw new Exception("Името на длъжността не може да бъде празно!");


            if (_repository.ExistsByTitle(position.Title))
                throw new Exception($"Длъжност с име '{position.Title}' вече съществува!");

            _repository.Save(position);
        }

        public List<Position> GetByDepartmentId(int departmentId)
        {
            return _repository.GetByDepartmentId(departmentId);
        }

        public List<Position> GetAllPositions()
        {
            return _repository.GetAll().ToList();
        }

        public Position GetById(int positionId)
        {
            return _repository.GetById(positionId);
        }
    }
}