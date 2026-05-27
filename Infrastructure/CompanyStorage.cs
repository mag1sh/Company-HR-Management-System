using System;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyHRManagementSystem.Employees.Infrastructure
{
    public class CompanyStorage : DbContext
    {
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\mssqllocaldb;" +
                    "Database=CompanyHRDb;" + 
                    "Integrated Security=True;" + 
                    "MultipleActiveResultSets=True;" + 
                    "Encrypt=True;" +
                    "TrustServerCertificate=True;" 
                );
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Name);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Email);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.PhoneNumber);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Address);

            base.OnModelCreating(modelBuilder);
        }
    }
}