using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Employee
    {
       // private static int _nextId = 1;
        public int Id { get;  set; }

        public FullName Name { get;  set; }
        public Email Email { get;  set; }
        public PhoneNumber PhoneNumber { get;  set; }
        public Address Address { get;  set; }

        public DateTime HireDate { get;  set; }
        public DateTime? TerminationDate { get;  set; }

        public EmployeeStatus Status { get;  set; }

        public int DepartmentId { get;  set; }
        public int PositionId { get;  set; }

        public  Department Department { get; set; }
        public  Position Position { get; set; }
        public  Salary Salary { get; set; }

        public  ICollection<Leave> Leaves { get; set; }
        public  ICollection<EmploymentHistory> EmploymentHistories { get; set; }
        public  ICollection<SalaryHistory> SalaryHistories { get; set; }

        // Constructor za sql
        protected Employee()
        {
           
        }

        public Employee( FullName name, Email email, PhoneNumber phoneNumber, Address address,
                        DateTime hireDate, int departmentId, int positionId) 
        {
            //Id = _nextId++;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            HireDate = hireDate;
            DepartmentId = departmentId;
            PositionId = positionId;
            Status = EmployeeStatus.Active;
        }
    }
}