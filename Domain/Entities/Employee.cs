using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Employee
    {
        public int Id { get; private set; }

        public FullName Name { get;  set; }
        public Email Email { get;  set; }
        public PhoneNumber PhoneNumber { get;  set; }
        public Address Address { get;  set; }

        public DateTime HireDate { get;  set; }
        public DateTime? TerminationDate { get;  set; }

        public EmployeeStatus Status { get;  set; }

        public int DepartmentId { get; private set; }
        public int PositionId { get; private set; }

        // Constructor
        public Employee(int id, FullName name, Email email, PhoneNumber phoneNumber, Address address,
                        DateTime hireDate, int departmentId, int positionId)
        {
            Id = id;
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
