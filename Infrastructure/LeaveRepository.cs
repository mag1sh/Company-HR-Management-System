using System;
using System.Collections.Generic;
using System.Linq;
using CompanyHRManagementSystem.Application.Interfaces;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Services.Interfaces;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileLeaveRepository : ILeaveRepository
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

        public IReadOnlyList<Leave> GetAll()
        {
            var db = _storage.Load();
            return db.Leaves;
        }

        public void Save(Leave leave)
        {
            var db = _storage.Load();

            if (leave.Id == 0)
            {
                var newLeave = new Leave(
                    leave.EmployeeId,
                    leave.LeaveType,
                    leave.StartDate,
                    leave.EndDate
                );

                db.Leaves.Add(newLeave);
            }
            else
            {
                var existing = db.Leaves.FirstOrDefault(l => l.Id == leave.Id);

                if (existing == null)
                    throw new Exception("Leave not found");

                existing.Reject(); 
            }

            _storage.Save(db);
        }

        public void Delete(int id)
        {
            var db = _storage.Load();

            var leave = db.Leaves.FirstOrDefault(l => l.Id == id);

            if (leave != null)
            {
                db.Leaves.Remove(leave);
                _storage.Save(db);
            }
        }

       
    }
}