using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        protected Email()
        {
        }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be empty");

            if (!value.Contains("@") || !value.Contains("."))
                throw new ArgumentException("Invalid email format");

            if (value.Length < 5)
                throw new ArgumentException("Email is too short");

            Value = value;

        }
        public override string ToString()
        {
            return Value;
        }
    }
}
