using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Number { get; }
        public PhoneNumber(string number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
