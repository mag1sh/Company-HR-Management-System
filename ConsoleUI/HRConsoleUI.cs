using System;
using System.Linq;
using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Services;
using Domain.Entities;

namespace CompanyHRManagementSystem.Employees.ConsoleUI
{
    public class HRConsoleUI
    {
        private readonly EmployeeService _employeeService;
        private readonly DepartmentService _departmentService;
        private readonly PositionService _positionService;
        private readonly LeaveService _leaveService;

        public HRConsoleUI(
             EmployeeService employeeService,
             DepartmentService departmentService,
             PositionService positionService,
             LeaveService leaveService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;
            _leaveService = leaveService;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== HR Management System =====");
                Console.WriteLine("1. Добавяне на нов служител");
                Console.WriteLine("2. Добавяне на отдел");
                Console.WriteLine("3. Добавяне на позиция");
                Console.WriteLine("4. Показване на всички активни служители");
                Console.WriteLine("5. Редактиране на служител");
                Console.WriteLine("6. Подаване на заявка за отпуск от служител");
                Console.WriteLine("7. Проверка за налични дни отпуск за служител");
                Console.WriteLine("8. Одобряване или отказване на заявка за отпуск");
                Console.WriteLine("9. Проверка за конфликт на отпуски в отдел");
                Console.WriteLine("10. Регистриране на болничен или неплатен отпуск");
                Console.WriteLine("11. Изчисляване на оставащи дни отпуск за служител");
                Console.WriteLine("12. Генериране на справка за служители по отдел или позиция");
                Console.WriteLine("13. Генериране на справка за отпуски по период");
                Console.WriteLine("14. Генериране за справка за текучество на персонала");
                Console.WriteLine("15. Проследяване на трудовия стаж на служител от компанията");
                Console.WriteLine("16. История за кадровите промени за конкретен служител");
                //Console.WriteLine("19. Show Salary History");
                //Console.WriteLine("20. Advanced Reference for Employees by Conditions");
                Console.WriteLine("X. Изход");

                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        AddDepartment();
                        break;
                    case "3":
                        AddPosition();
                        break;
                    case "4":
                        ShowAllActiveEmployees();
                        break;
                    case "5":
                        EmployeesControl();
                        // DepartmentsEmployeesControl();
                        break;
                    case "6":
                        VacationRequestByEmployee();
                        // ShowSalaryhistory();
                        break;
                    case "7":
                      //  VacationRequests();
                        break;
                    case "8":
                       // vacationDaysForEveryEmployee();
                        break;
                    case "9":
                       // ApproveOrDenyVacantionRequsts();
                        break;
                    case "10":
                       // RegisterLeaveTypeRequests();
                        break;
                    case "11":
                       // ReferenceForEveryEmployeeByCondition();
                        break;
                    case "12":
                         ShowEmployeesByDepartmentOrPosition();
                        // ShowEmploymentHistory();
                        break;
                    case "13": 
                        //
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

        private void VacationRequestByEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Подаване на заявка за отпуск от служител ---");
            Console.Write("Enter Employee id: ");
            int employeeId = int.Parse(Console.ReadLine());
            Console.Write("Enter Start Date (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter End Date (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Leave Type (Vacation/Sick/Unpaid): ");
            string leaveTypeInput = Console.ReadLine();
            LeaveType leaveType;
            switch (leaveTypeInput.ToLower())
            {
                case "vacation":
                    leaveType = LeaveType.Vacation;
                    break;
                case "sick":
                    leaveType = LeaveType.Sick;
                    break;
                case "unpaid":
                    leaveType = LeaveType.Unpaid;
                    break;
                default:
                    Console.WriteLine("Invalid leave type!");
                    return;
            }
            var leave = new Leave(employeeId, leaveType, startDate, endDate);
            try
            {
                _leaveService.RequestLeave(leave);
                Console.WriteLine("Leave request submitted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ShowEmployeesByDepartmentOrPosition()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на служители по отдел или позиция ---");
            Console.WriteLine("1. Показване на служител по отдел");
            Console.WriteLine("2. Показване на служител по позиция");
            Console.WriteLine("X. Назад към главното меню");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowEmployeesByDepartment();
                    break;
                case "2":
                    ShowEmployeesByPosition();
                    break;
                case "X":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }


        private void ShowEmployeesByDepartment()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на служители по отдел ---");
            Console.Write("Enter Department id: ");
            int departmentId = int.Parse(Console.ReadLine());
            var employees = _employeeService.GetAllActiveEmployees()
                .Where(e => e.DepartmentId == departmentId)
                .ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found for the specified department.");
                return;
            }
            Console.WriteLine($"Employees in Department {departmentId}:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Name} | {employee.Email} | {employee.PhoneNumber.Number}");
            }
            Console.ReadLine();
        }

        private void ShowEmployeesByPosition()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на служители по позиция ---");
            Console.Write("Enter Position id: ");
            int positionId = int.Parse(Console.ReadLine());
            var employees = _employeeService.GetAllActiveEmployees()
                .Where(e => e.PositionId == positionId)
                .ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found for the specified position.");
                return;
            }
            Console.WriteLine($"Employees in Position {positionId}:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Name} | {employee.Email} | {employee.PhoneNumber.Number}");
            }
            Console.ReadLine();
        }

        private void EmployeesControl()
        {
            Console.Clear();
            Console.WriteLine("--- Employees Control Menu ---");
            Console.WriteLine("1. Редактиране на лични данни на служител");
            Console.WriteLine("2. Деактивиране / Уволняване на служител");
            Console.WriteLine("3. Смяна на отдел и позиция на служител");
            Console.WriteLine("4. Смяна на основна заплата на служител");
            Console.WriteLine("X. Назад към главното меню");
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
                case "4":
                    ChangeEmployeeSalary();
                    break;
                case "X":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
        private void AddPosition()
        {
            Console.WriteLine("\n--- Add New Position ---");

            Console.Write("Position Title (e.g. C# Developer): ");
            string title = Console.ReadLine();

            Console.Write("Position Description: ");
            string description = Console.ReadLine();

            Console.Write("Base Salary for this position (euro): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalary))
            {
                Console.WriteLine("Invalid salary format! Position creation aborted.");
                return;
            }

            try
            {    
                var position = new Position(title, description, baseSalary);

                _positionService.AddPosition(position);
                Console.WriteLine($"Position added successfully! Its REAL ID is: {position.Id}");
                //Console.WriteLine($"Position '{title}' added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ChangeEmployeeSalary()
        {
            try
            {
                Console.Write("Enter Employee id: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);

                Console.Write("Enter new Salary (euro): ");
                decimal newSalary = decimal.Parse(Console.ReadLine());
                //if (!decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                //{
                //    Console.WriteLine("Invalid salary format!");
                //    return;
                //}

                employee.Salary.Amount = newSalary;
                _employeeService.UpdateEmployee(employee);
                Console.WriteLine("Employee's salary updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ChangeEmployeeDepartmentAndPosition()
        {
            try
            {
                Console.Write("Enter Employee id: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);

               
                Console.WriteLine($"Current Department: {_departmentService.GetById(employee.DepartmentId).Name}");
                Console.WriteLine($"Current Position: {_positionService.GetById(employee.PositionId).Title}");

                Console.Write("Enter new Department id: ");
                int newDepartmentId = int.Parse(Console.ReadLine());

                Console.Write("Enter new Position id: ");
                int newPositionId = int.Parse(Console.ReadLine());
                _departmentService.GetById(newDepartmentId);
                _positionService.GetById(newPositionId);

                employee.DepartmentId = newDepartmentId;
                employee.PositionId = newPositionId;
                _employeeService.UpdateEmployee(employee);
                Console.WriteLine("Employee's department and position updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void EditEmployee()
        {
            try
            {
                Console.Write("Enter Employee id: ");
                int employeeId = int.Parse(Console.ReadLine());

                _employeeService.GetById(employeeId);

                Employee updatedemployee = GetEmployeInfo();
                updatedemployee.Id = employeeId;

                _employeeService.UpdateEmployee(updatedemployee);
                Console.WriteLine("Employee updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void DeactivateOrFireEmployee()
        {
            Console.Write("Enter Employee id: ");
            int employeeId = int.Parse(Console.ReadLine());
            _employeeService.DeactivateEmployee(employeeId);
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
            Console.WriteLine($"Department added successfully! Its REAL ID is: {department.DepartmentId}");
            // Console.WriteLine("Department added successfully!");
            Console.ReadLine();
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
    
        private void ShowAllActiveEmployees()
        {
            var employees = _employeeService.GetAllActiveEmployees();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Department.Name} | {employee.Name} | {employee.Salary.Amount} | {employee.Email}");
            }
            Console.ReadLine();
        }
    } 
}

