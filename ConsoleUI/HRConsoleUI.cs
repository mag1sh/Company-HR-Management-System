using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services;

namespace CompanyHRManagementSystem.Employees.ConsoleUI
{
    public class HRConsoleUI
    {

        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;

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
                        AddDepartment();
                        break;
                    case "4":
                        EmployeesControl();
                        break;
                    case "5":
                        DepartmentsEmployeesControl();
                        break;
                    case "6":
                        ShowSalaryhistory();
                        break;
                    case "7":
                        VacationRequests();
                        break;
                    case "8":
                        vacationDaysForEveryEmployee();
                        break;
                    case "8":
                        ApproveOrDenyVacantionRequsts();
                        break;
                    case "9":
                        RegisterLeaveTypeRequests();
                        break;
                    case "9":
                        ReferenceForEveryEmployeeByCondition();
                        break;
                    case "9":
                        ShowEmploymentHistory();
                        break;
                    case "X":
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void EmployeesControl()
        {
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    EditEmployee();
                    break;
                case "2":
                    DeactivateOrFireEmployee();
                    break;
                case "3":
                    ChangeEmployeeDepartmentAndPosition();
                    break;
                case "3":
                    ChangeEmployeeSalary();
                    break;

                case "X":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        private void EditEmployee()
        {
            Console.Write("Enter Employee id: ");
            int employeeId = int.Parse(Console.ReadLine());
            Employee updatedemployee = GetEmployeInfo();
            updatedemployee.Id = employeeId;
            _employeeService.UpdateEmployee(updatedemployee);
        }

        private void AddDepartment()
        {
            Console.Write("Department Name: ");
            string departmentName = Console.ReadLine();

            Console.Write("Department Description: ");
            string departmnetDescription = Console.ReadLine();

            var department = new Department(
               departmentName,
               departmnetDescription);

            _departmentService.AddDepartment(department);
            Console.WriteLine("Department added successfully!");
            
        }

       public Employee GetEmployeInfo()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Which department id: ");
            int deparmentId = int.Parse(Console.ReadLine());

            Console.Write("Position id: ");
            int posionId = int.Parse(Console.ReadLine());

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



            FullName name = new FullName(firstName, lastName);
            DateTime hiredate = DateTime.Now;
            PhoneNumber phoneNumber = new PhoneNumber(phone);


            var employee = new Employee(
                 name,
                email,
                phoneNumber,
                address,
                hiredate,
                deparmentId,
                posionId
                );
            return employee;
        }

        private void AddEmployee()
        {
            Employee employee = GetEmployeInfo();

            _employeeService.AddEmployee(employee);

            Console.WriteLine("Employee added successfully!");
        }
    
        private void ShowAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();

            foreach (var employee in employees)
            {
                Console.WriteLine(
                    $"{employee.Id} | " +
                    $"{employee.Name.ToString()} | " +
                    $"{employee.Email}");
            }
        }



    } 
}

