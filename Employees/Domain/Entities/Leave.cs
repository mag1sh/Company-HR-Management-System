using CompanyHRManagementSystem.Employees.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Leave
    {
        public int Id { get; private set; }
        public int EmployeeId { get; private set; }
        public LeaveType LeaveType { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int CountDays { get; set; }
        public LeaveStatus Status { get; private set; }

    }
}
