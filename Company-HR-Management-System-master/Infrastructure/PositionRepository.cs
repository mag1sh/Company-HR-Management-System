using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Infrastructure;
using Domain.Entities;

namespace CompanyHRManagementSystem.Infrastructure
{
    public class PositionRepository
    {
        private readonly CompanyStorage _context;
        public PositionRepository(CompanyStorage context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Position GetById(int id)
        {
            foreach (var position in _context.Positions)
            {
                if (position.Id == id)
                {
                    return position;
                }
            }
            throw new Exception("Position not found");
        }

        public IReadOnlyList<Position> GetAll()
        {
            List<Position> allPositions = new List<Position>();
            foreach (var position in _context.Positions)
            {
                allPositions.Add(position);
            }
            return allPositions;
        }

        public void Save(Position position)
        {
            if (position.Id == 0)
            {
                _context.Positions.Add(position);
            }
            else
            {
                Position existing = null;
                foreach (var p in _context.Positions)
                {
                    if (p.Id == position.Id)
                    {
                        existing = p;
                        break;
                    }
                }

                if (existing == null)
                    throw new Exception("Position not found");

                existing.Title = position.Title;
                existing.Description = position.Description;
                existing.BaseSalary = position.BaseSalary;
            }

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Position positionToDelete = null;
            foreach (var p in _context.Positions)
            {
                if (p.Id == id)
                {
                    positionToDelete = p;
                    break;
                }
            }

            if (positionToDelete != null)
            {
                _context.Positions.Remove(positionToDelete);
                _context.SaveChanges(); 
            }
        }
    }
}