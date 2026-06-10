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
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    //"Data Source=(localdb)\\mssqllocaldb;" +
                    //"Database=CompanyHRDb;" +
                    //"Integrated Security=True;" +
                    //"MultipleActiveResultSets=True;" +
                    //"Encrypt=False;" +
                    //"TrustServerCertificate=True;"

                    // "Data Source=K207\\SQLEXPRESS;Database=CompanyHRDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;" k7
                    "Data Source=DESKTOP-VGLENU1\\SQLEXPRESS;Database=CompanyHRDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Name);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Email);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.PhoneNumber, p =>
            {
                p.Property(x => x.Number).HasColumnName("PhoneNumber_Number");
            });
            modelBuilder.Entity<Employee>().OwnsOne(e => e.Address);

            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Salary)
                        .WithOne(s => s.Employee)
                        .HasForeignKey<Salary>(s => s.EmployeeId);

            modelBuilder.Entity<Position>()
                        .HasOne(p => p.Department)
                        .WithMany()
                        .HasForeignKey(p => p.DepartmentId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmploymentHistory>()
                        .HasOne(e => e.Employee)
                        .WithMany(e => e.EmploymentHistories)
                        .HasForeignKey(e => e.EmployeeId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SalaryHistory>()
                        .HasOne(s => s.Employee)
                        .WithMany(e => e.SalaryHistories)
                        .HasForeignKey(s => s.EmployeeId)
                        .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}