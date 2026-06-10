using System;

namespace CompanyHRManagementSystem.Employees.Domain.ValueObjects
{
    public class PhoneNumber
    {
        public string Number { get; }

        protected PhoneNumber()
        {
        }

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