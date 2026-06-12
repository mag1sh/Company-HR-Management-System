using System.Collections.Generic;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Department
    {
        public int DepartmentId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public  ICollection<Employee> Employees { get; private set; }

        // public List<Employee> Employees { get; private set; }

        protected Department()
        {

        }

        public Department(string name, string description)
        {
            Name = name;
            Description = description;
            Employees = new List<Employee>();
        }
    }
}