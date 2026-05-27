using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Services;

namespace CompanyHRManagementSystem.Employees.ConsoleUI
{
    public class HRConsoleUI
    {
       
            private readonly EmployeeService _employeeService;

            public HRConsoleUI(EmployeeService employeeService)
            {
                _employeeService = employeeService;
            }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("===== HR Management System =====");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Show All Employees");
                Console.WriteLine("3. Exit");

                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;

                    case "2":
                        ShowAllEmployees();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void ShowAllEmployees()
        {
            throw new NotImplementedException();
        }

        private void AddEmployee()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Which department: ");
            string deparment = Console.ReadLine();

            Console.Write("Position: ");
            string posion = Console.ReadLine();

            Console.Write("Email: ");
            string emailInput = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phone = Console.ReadLine();

            Console.Write("Country: ");
            string country = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("Postal Code: ");
            string postalCode = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("Street Number: ");
            string streetNumber = Console.ReadLine();

            var email = new Email(emailInput);

            var address = new Address(
                country,
                city,
                postalCode,
                street,
                streetNumber);



            string name = $"{firstName} {lastName}";
            DateTime hiredate = DateTime.Now;
            Department department = new Department(deparment, "description");


            var employee = new Employee(
                 name,
                email,
                phone,
                address,
                hiredate
                );

            _employeeService.AddEmployee(employee);

            Console.WriteLine("Employee added successfully!");
        }
    }
    
}
