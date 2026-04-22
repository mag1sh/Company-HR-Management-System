using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Employees.Domain.ValueObjects
{
    public class Address
    {
        public string Country { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }

        public Address(string country, string city, string postalCode, string street, string streetNumber)
        {

        }
    }
}
