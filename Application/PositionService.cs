using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace CompanyHRManagementSystem.Application
{
    public class PositionService
    {
        private readonly List<Position> _positions = new List<Position>();
        private int _nextId = 1;

        
        public void AddPosition(Position position)
        {
            if (string.IsNullOrWhiteSpace(position.Title))
                throw new Exception("Името на длъжността не може да бъде празно!");

            
            foreach (var p in _positions)
            {
                if (p.Title.Equals(position.Title, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception($"Длъжност с име '{position.Title}' вече съществува!");
                }
            }

            _positions.Add(position);
        }

        
        public List<Position> GetAllPositions()
        {
            return _positions;
        }

        
        public Position GetById(int positionId)
        {
            foreach (var p in _positions)
            {
                if (p.Id == positionId) 
                {
                    return p;
                }
            }

            throw new Exception("Избраната длъжност/позиция не съществува!");
        }
    }
}
