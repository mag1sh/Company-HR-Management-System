using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Infrastructure
{
    public class PositionRepository : IPositionRepository
    {
        private readonly CompanyStorage _context;

        public PositionRepository(CompanyStorage context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Position GetById(int id)
        {
            var position = _context.Positions.FirstOrDefault(p => p.Id == id);
            if (position == null)
                throw new Exception("Position not found");
            return position;
        }

        public IReadOnlyList<Position> GetAll()
        {
            return _context.Positions.ToList();
        }

        public void Save(Position position)
        {
            if (position.Id == 0)
            {
                _context.Positions.Add(position);
            }
            else
            {
                var existing = _context.Positions.FirstOrDefault(p => p.Id == position.Id);
                if (existing == null)
                    throw new Exception("Position not found");

                existing.Title = position.Title;
                existing.Description = position.Description;
                existing.BaseSalary = position.BaseSalary;
            }

            _context.SaveChanges();
        }

        public List<Position> GetByDepartmentId(int departmentId)
        {
            return _context.Positions
                .Where(p => p.DepartmentId == departmentId)
                .ToList();
        }

        public bool ExistsByTitle(string title)
        {
            return _context.Positions
                .Any(p => p.Title.ToLower() == title.ToLower());
        }
    }
}