using System;
using System.Collections.Generic;
using System.Linq;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Services.Interfaces;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class GetById : ILeaveRepository
    {
        private readonly FileStorage _storage;

        public FileLeaveRepository(FileStorage storage)
        {
            _storage = storage;
        }

        public Leave GetById(int id)
        {
            var db = _storage.Load();

            return db.Leaves.FirstOrDefault(l => l.Id == id)
                   ?? throw new Exception("Leave not found");
        }

       
    }
}