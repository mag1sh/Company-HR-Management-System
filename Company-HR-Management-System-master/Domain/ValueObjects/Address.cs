using System;

namespace CompanyHRManagementSystem.Employees.Domain.ValueObjects
{
    public class Address
    {
        public string Country { get; private set; }

        public string City { get; private set; }

        public string PostalCode { get; private set; }

        public string Street { get; private set; }

        public string StreetNumber { get; private set; }

        protected Address()
        {
        }

        public Address(string country, string city, string postalCode, string street, string streetNumber)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty");

            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            StreetNumber = streetNumber;
        }

        public override string ToString()
        {
            return $"{Street} {StreetNumber}, {City}, {PostalCode}, {Country}";
        }
    }
}