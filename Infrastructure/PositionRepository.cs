using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

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

            return db.Positions.FirstOrDefault(p => p.Id == id)
                   ?? throw new Exception("Position not found");
        }

        public IReadOnlyList<Position> GetAll()
        {
            var db = _storage.Load();
            return db.Positions;
        }

        public void Save(Position position)
        {
            var db = _storage.Load();

            if (position.Id == 0)
            {
                var newPosition = new Position(
                    db.NextId++,
                    position.Title,
                    position.Description,
                    position.BaseSalary
                );

                db.Positions.Add(newPosition);
            }
            else
            {
                var existing = db.Positions.FirstOrDefault(p => p.Id == position.Id);

                if (existing == null)
                    throw new Exception("Position not found");

                db.Positions.Remove(existing);
                db.Positions.Add(position);
            }

            _storage.Save(db);
        }

        public void Delete(int id)
        {
            var db = _storage.Load();

            var position = db.Positions.FirstOrDefault(p => p.Id == id);

            if (position != null)
            {
                db.Positions.Remove(position);
                _storage.Save(db);
            }
        }
    }
}