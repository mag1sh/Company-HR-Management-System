using System;
using System.Collections.Generic;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class CompanyStorage
    {
        public int NextId { get; set; } = 1;

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}