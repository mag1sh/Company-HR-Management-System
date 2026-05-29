using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int EmployeeId { get; set; }
        public  Employee Employee { get; set; }
    }
}
