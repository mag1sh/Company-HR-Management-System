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
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number cannot be empty.");
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
