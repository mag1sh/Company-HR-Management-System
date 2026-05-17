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

    }
}