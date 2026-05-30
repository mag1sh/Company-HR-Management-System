using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Employees.Infrastructure;
using Domain.Entities;

namespace CompanyHRManagementSystem.Application
{
    public class PositionService
    {
       
        
            private readonly CompanyStorage _storage;

          
            public PositionService(CompanyStorage storage)
            {
                _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            }

           
            public void AddPosition(Position position)
            {
                if (string.IsNullOrWhiteSpace(position.Title))
                    throw new Exception("Името на длъжността не може да бъде празно!");


                foreach (var p in _storage.Positions)
                {
                    if (p.Title.Equals(position.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception($"Длъжност с име '{position.Title}' вече съществува!");
                    }
                }

                _storage.Positions.Add(position);
                _storage.SaveChanges(); 
            }

            public List<Position> GetAllPositions()
            {
                return _storage.Positions.ToList();
            }

           
            public Position GetById(int positionId)
            {
                foreach (var p in _storage.Positions)
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

