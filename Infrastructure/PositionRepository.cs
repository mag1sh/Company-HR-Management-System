using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class PositionRepository
    {
        private readonly FileStorage _storage;

        public PositionRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public Position GetById(int id)
        {
            var db = _storage.Load();

            return db.Positions.FirstOrDefault(p => p.PositionId == id)
                   ?? throw new Exception("Position not found");
        }

        public IReadOnlyList<Position> GetAll()
        {
            var db = _storage.Load();
            return db.Positions;
        }

        
    }
}