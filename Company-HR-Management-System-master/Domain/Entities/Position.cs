using CompanyHRManagementSystem.Employees.Domain.Entities;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BaseSalary { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public  ICollection<Employee> Employees { get; set; }

        protected Position()
        {
           
        }

        public Position( string title, string description, decimal baseSalary, int departmentId) : this()
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (baseSalary < 0)
                throw new ArgumentException("Salary cannot be negative");

            Title = title;
            Description = description;
            BaseSalary = baseSalary;
            DepartmentId = departmentId;
        }

        public override string ToString()
        {
            return $"{Title} - {BaseSalary} euro";
        }
    }
}