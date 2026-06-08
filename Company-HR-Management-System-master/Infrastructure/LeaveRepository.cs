using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class FileLeaveRepository : ILeaveRepository
    {
        private readonly CompanyStorage _context;

        public FileLeaveRepository(CompanyStorage context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Leave GetById(int id)
        {
            var leave = _context.Leaves.FirstOrDefault(l => l.Id == id);
            if (leave == null)
                throw new Exception("Leave not found");
            return leave;
        }

        public IReadOnlyList<Leave> GetAll()
        {
            return _context.Leaves.ToList();
        }

        public void Save(Leave leave)
        {
            if (leave.Id == 0)
            {
                _context.Leaves.Add(leave);
            }
            else
            {
               
                Leave existing = null;
                foreach (var l in _context.Leaves)
                {
                    if (l.Id == leave.Id)
                    {
                        existing = l;
                        break;
                    }
                }

                if (existing == null)
                    throw new Exception("Leave not found");

               
                existing.Status = leave.Status;

                
                existing.LeaveType = leave.LeaveType;
                existing.DaysCount = leave.DaysCount;
            }

           
            _context.SaveChanges();
        }

        
        public void Delete(int id)
        {
            Leave leaveToDelete = null;
            foreach (var l in _context.Leaves)
            {
                if (l.Id == id)
                {
                    leaveToDelete = l;
                    break;
                }
            }

            if (leaveToDelete != null)
            {
                _context.Leaves.Remove(leaveToDelete);
                _context.SaveChanges(); 
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IReadOnlyList<Leave> GetByEmployeeId(int employeeId)
        {
            List<Leave> employeeLeaves = new List<Leave>();
            foreach (var l in _context.Leaves)
            {
                if (l.EmployeeId == employeeId)
                {
                    employeeLeaves.Add(l);
                }
            }
            return employeeLeaves;
        }


        public IReadOnlyList<Leave> GetByDepartmentId(int departmentId)
        {
            List<int> employeeIdsInDept = new List<int>();
            foreach (var employee in _context.Employees)
            {
                if (employee.DepartmentId == departmentId)
                {
                    employeeIdsInDept.Add(employee.Id);
                }
            }

           
            List<Leave> departmentLeaves = new List<Leave>();
            foreach (var leave in _context.Leaves)
            {
                if (employeeIdsInDept.Contains(leave.EmployeeId))
                {
                    departmentLeaves.Add(leave);
                }
            }

            return departmentLeaves;
        }
    }
}