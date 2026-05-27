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
            private readonly FileStorage _emplopyeeStorage;

            public FileEmployeeRepository(FileStorage storage)
            {
                _emplopyeeStorage = storage;
            }

            public IReadOnlyList<Employee> GetAll()
            {
                var db = _emplopyeeStorage.Load();

                return db.Employees;
            }

            public Employee GetById(int id)
            {
                var db = _emplopyeeStorage.Load();

                foreach (var account in db.Employees)
                {
                    if (account.Id == id)
                    {
                        return account;
                    }
                }

                throw new Exception("Account not found");
            }

            public void Save(Employee employee)
            {
                var db = _emplopyeeStorage.Load();
                if (employee.Id == 0)
                {
                    var newEmployee = new Employee(
                        employee.Name,
                        employee.Email,
                        employee.PhoneNumber,
                        employee.Address,
                        employee.HireDate,
                        employee.DepartmentId,
                        employee.PositionId
                        );
                    db.Employees.Add(newEmployee);
                }
                else
                {
                    bool isFound = false;
                    for (int i = 0; i < db.Employees.Count; i++)
                    {
                        if (db.Employees[i].Id == employee.Id)
                        {
                            db.Employees[i] = employee;
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        throw new Exception("Account not found");
                    }
                }
                _emplopyeeStorage.Save(db);
            }
        }
}
