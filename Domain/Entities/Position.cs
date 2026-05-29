using CompanyHRManagementSystem.Employees.Domain.Entities;
using System;

namespace Domain.Entities
{
    public class Position
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal BaseSalary { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

        protected Position()
        {
            Employees = new HashSet<Employee>();
        }

        public Position(int id, string title, string description, decimal baseSalary) : this()
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (baseSalary < 0)
                throw new ArgumentException("Salary cannot be negative");

            Id = id;
            Title = title;
            Description = description;
            BaseSalary = baseSalary;
        }

        public override string ToString()
        {
            return $"{Title} - {BaseSalary} lv.";
        }
    }
}