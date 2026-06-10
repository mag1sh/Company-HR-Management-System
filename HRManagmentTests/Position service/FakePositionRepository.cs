using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace CompanyHRManagementSystem.Tests.Position_service
{
    public class FakePositionRepository : IPositionRepository
    {
        public List<Position> Positions = new List<Position>();

        public void Save(Position position)
        {
            position.Id = Positions.Count + 1;
            Positions.Add(position);
        }

        public IReadOnlyList<Position> GetAll()
        {
            return Positions;
        }

        public Position GetById(int positionId)
        {
            var position = Positions.FirstOrDefault(p => p.Id == positionId);
            if (position == null)
                throw new Exception("Позицията не е намерена!");
            return position;
        }

        public bool ExistsByTitle(string title)
        {
            return Positions.Any(p => p.Title.ToLower() == title.ToLower());
        }

        public List<Position> GetByDepartmentId(int departmentId)
        {
            return Positions.Where(p => p.DepartmentId == departmentId).ToList();
        }
    }
}