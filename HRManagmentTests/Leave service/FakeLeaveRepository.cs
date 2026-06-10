using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using System.Linq;
namespace CompanyHRManagementSystem.Tests.Leave_service
{
    public class FakeLeaveRepository : ILeaveRepository
    {
        public List<Leave> Leaves = new List<Leave>();

        public Leave GetById(int id)
        {
            var leave = Leaves.FirstOrDefault(l => l.Id == id);
            if (leave == null)
                throw new Exception("Leave not found");
            return leave;
        }

        public IReadOnlyList<Leave> GetAll() => Leaves;

        public void Save(Leave leave)
        {
            if (leave.Id == 0)
            {
                leave.Id = Leaves.Count + 1;
                Leaves.Add(leave);
            }
            else
            {
                var existing = Leaves.FirstOrDefault(l => l.Id == leave.Id);
                if (existing != null)
                    existing.Status = leave.Status;
            }
        }

        public void Delete(int id)
        {
            var leave = Leaves.FirstOrDefault(l => l.Id == id);
            if (leave != null)
                Leaves.Remove(leave);
        }

        public void SaveChanges() { }

        public IReadOnlyList<Leave> GetByEmployeeId(int employeeId)
        {
            return Leaves.Where(l => l.EmployeeId == employeeId).ToList();
        }

        public IReadOnlyList<Leave> GetByDepartmentId(int departmentId)
        {
            return new List<Leave>();
        }
    }
}