using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }
    }
    
}
