using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Infrastructure
{
         public class FileEmployeeRepository : IEmployeeRepository
        {
        private readonly CompanyStorage _context;

      
        public FileEmployeeRepository(CompanyStorage context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        
        public IReadOnlyList<Employee> GetAll()
        {
           
            return _context.Employees.ToList();
        }

        
        public Employee GetById(int id)
        {
            foreach (var employee in _context.Employees)
            {
                if (employee.Id == id)
                {
                    return employee;
                }
            }

            throw new Exception("Employee not found in database");
        }

        
        public void Save(Employee employee)
        {
            if (employee.Id == 0)
            {
               
                _context.Employees.Add(employee);
            }
            else
            {
               
                Employee existingEmployee = null;
                foreach (var e in _context.Employees)
                {
                    if (e.Id == employee.Id)
                    {
                        existingEmployee = e;
                        break;
                    }
                }

                if (existingEmployee == null)
                {
                    throw new Exception("Employee not found for update");
                }

              
                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Address = employee.Address;
                existingEmployee.DepartmentId = employee.DepartmentId;
                existingEmployee.PositionId = employee.PositionId;
                existingEmployee.Status = employee.Status;
            }

           
            _context.SaveChanges();
        }
    }
}
