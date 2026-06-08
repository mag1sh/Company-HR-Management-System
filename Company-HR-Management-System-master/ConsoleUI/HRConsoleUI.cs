using System;
using System.Configuration;
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
                Console.WriteLine("9. Проверка за конфликт на отпуски в отдел"); //KAKVO TRQ STANE TUKA
                Console.WriteLine("10. Регистриране на болничен или неплатен отпуск"); //nznznz
                Console.WriteLine("11. Изчисляване на оставащи дни отпуск за служител"); //tva mai e 7
                Console.WriteLine("12. Генериране на справка за служители по отдел или позиция");
                Console.WriteLine("13. Генериране на справка за отпуски по период");
                Console.WriteLine("14. Генериране за справка за текучество на персонала");       //kakwo trq stava tuka lud
                Console.WriteLine("15. Проследяване на трудовия стаж на служител от компанията");
                Console.WriteLine("16. История за кадровите промени за конкретен служител"); //NZ KAKWO TRQ STANE TUKA
                Console.WriteLine("17. Показване на историята на заплатите на служител"); //NZ KAKWO TRQ STANE TUKA
                Console.WriteLine("18. Разширена справка за служители по условия");
                Console.WriteLine("X. Изход");

                Console.Write("Choose option: ");

                string choice = Console.ReadLine().ToUpper();

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
                        break;
                    case "6":
                        VacationRequestByEmployee();
                        // ShowSalaryhistory();
                        break;
                    case "7":
                        CheckAvailableVacationDaysForEmployee();
                        //  VacationRequests();
                        break;
                    case "8":
                        ApproveOrDenyVacantionRequsts();
                        // vacationDaysForEveryEmployee();
                        break;
                    case "9":
                        CheckForVacationConflictsInDepartment();
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
                        ShowLeaveRequestsByPeriod();
                        break;
                    case "14":
                        GenerateEmployeeTurnoverReport();
                        break;
                    case "15":
                        TrackStajNaEmployee();
                        break;
                    case "16":
                       //ShowEmployeeChangeHistory();
                        break;
                    case "17":
                        //ShowSalaryHistory();
                        break;
                    case "18":
                        ShowAdvancedReferenceForEmployeesByConditions();
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

        private void ShowAdvancedReferenceForEmployeesByConditions()
        {
            Console.WriteLine("--- Разширена справка за служители по условия ---");
            Console.Write("Въведете минимална заплата (euro): ");
            decimal minSalary = decimal.Parse(Console.ReadLine());
            Console.Write("Въведете максимална заплата (euro): ");
            decimal maxSalary = decimal.Parse(Console.ReadLine());
            Console.Write("Въведете id на отдел (или оставете празно за всички): ");
            string departmentInput = Console.ReadLine();
            int? departmentId = string.IsNullOrWhiteSpace(departmentInput) ? (int?)null : int.Parse(departmentInput);
            Console.Write("Въведете id на позиция (или оставете празно за всички): ");
            string positionInput = Console.ReadLine();
            int? positionId = string.IsNullOrWhiteSpace(positionInput) ? (int?)null : int.Parse(positionInput);
            var employees = _employeeService.GetAllActiveEmployees()
                .Where(e => e.Salary.Amount >= minSalary && e.Salary.Amount <= maxSalary)
                .Where(e => !departmentId.HasValue || e.DepartmentId == departmentId.Value)
                .Where(e => !positionId.HasValue || e.PositionId == positionId.Value)
                .ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("Няма намерени служители, отговарящи на посочените условия.");
                return;
            }
            Console.WriteLine($"Служители с заплата между {minSalary} и {maxSalary}, от отдел {(departmentId.HasValue ? departmentId.ToString() : "всички")} и позиция {(positionId.HasValue ? positionId.ToString() : "всички")}:");
            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Department.Name} | {employee.Position.Title} | {employee.Name} | {employee.Salary.Amount} | {employee.Email}");
            }
            Console.ReadLine();
        }

        //private void ShowSalaryHistory()
        //{
        //    Console.WriteLine("--- История на заплатите на служител ---");
        //    Console.Write("Въведете id на служител: ");
        //    int employeeId = int.Parse(Console.ReadLine());
        //    var salaryHistory = _employeeService.GetSalaryHistory(employeeId);
        //    if (salaryHistory.Count == 0)
        //    {
        //        Console.WriteLine("Няма намерени записи за заплатите на този служител.");
        //        return;
        //    }
        //    Console.WriteLine($"История на заплатите за служител с id {employeeId}:");
        //    foreach (var record in salaryHistory)
        //    {
        //        Console.WriteLine($"{record.ChangeDate:yyyy-MM-dd} | {record.OldSalary} -> {record.NewSalary} | {record.Description}");
        //    }
        //    Console.ReadLine();
        //}

        //private void ShowEmployeeChangeHistory()
        //{
        //    Console.WriteLine("--- История за кадровите промени за конкретен служител ---");
        //    Console.Write("Въведете id на служител: ");
        //    int employeeId = int.Parse(Console.ReadLine());
        //    var history = _employeeService.GetEmployeeChangeHistory(employeeId);
        //    if (history.Count == 0)
        //    {
        //        Console.WriteLine("Няма намерени кадрови промени за този служител.");
        //        return;
        //    }
        //    Console.WriteLine($"Кадрови промени за служител с id {employeeId}:");
        //    foreach (var change in history)
        //    {
        //        Console.WriteLine($"{change.ChangeDate:yyyy-MM-dd} | {change.ChangeType} | {change.Description}");  //KAK SE PRAVI
        //    }
        //    Console.ReadLine();
        //}

        private void TrackStajNaEmployee()
        {
            Console.WriteLine("--- Проследяване на трудовия стаж на служител от компанията ---");
            Console.Write("Въведете id на служител: ");
            int employeeId = int.Parse(Console.ReadLine());
            Employee employee = _employeeService.GetById(employeeId);
            if(employee == null)
            {
                Console.WriteLine("Служителят с въведения id не съществува.");
                return;
            }
            TimeSpan staj = (employee.TerminationDate ?? DateTime.Now) - employee.HireDate;
            Console.WriteLine($"Служител {employee.Name} има {staj.Days / 365} години, {(staj.Days % 365) / 30} месеца и {(staj.Days % 365) % 30} дни трудов стаж в компанията.");
            Console.ReadLine();
        }

        private void GenerateEmployeeTurnoverReport()
        {
            //kakwo trq stava tuka lud
            //Console.WriteLine("--- Справка за текучество на персонала ---");
            //var turnoverReport = _employeeService.GenerateEmployeeTurnoverReport();
            //Console.WriteLine($"Година | Брой наети | Брой напуснали | Текучество (%)");
            //foreach (var entry in turnoverReport)
            //{
            //    Console.WriteLine($"{entry.Year} | {entry.HiredCount} | {entry.LeftCount} | {entry.TurnoverRate:F2}%");
            //}
            //Console.ReadLine();
        }

        private void CheckForVacationConflictsInDepartment()
        {
            //Console.Clear();
            //Console.WriteLine("--- Проверка за конфликт на отпуски в отдел ---");
            //Console.Write("Enter Department id: ");
            //int departmentId = int.Parse(Console.ReadLine());
            //var conflicts = _leaveService.GetVacationConflictsInDepartment(departmentId);
            //if (conflicts.Count == 0)
            //{
            //    Console.WriteLine("No vacation conflicts found in this department.");
            //}
            //else
            //{
            //    Console.WriteLine("Vacation conflicts in this department:");
            //    foreach (var conflict in conflicts)
            //    {
            //        var employee1 = _employeeService.GetById(conflict.EmployeeId1);
            //        var employee2 = _employeeService.GetById(conflict.EmployeeId2);
            //        Console.WriteLine($"Conflict between {employee1.Name} and {employee2.Name} from {conflict.StartDate:yyyy-MM-dd} to {conflict.EndDate:yyyy-MM-dd}");
            //    }
            //}
            //Console.ReadLine(); - TAV NE E VAZMOJNO DA SE TESTVA, ZASHTOTO NE E IMPLEMENTIRANO V SERVICE-A  -copilot komentar btw ne e ot men
        }

        private void ShowLeaveRequestsByPeriod()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на заявки за отпуск по период ---");
            Console.Write("Въведи начална дата (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Въведи крайна дата (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            var leaves = _leaveService.GetLeavesByPeriod(startDate, endDate);
            if (leaves.Count == 0)
            {
                Console.WriteLine("Няма намерени заявки за отпуск за посочения период.");
                return;
            }
            Console.WriteLine($"Заявки за отпуск от {startDate:yyyy-MM-dd} до {endDate:yyyy-MM-dd}:");
            foreach (var leave in leaves)
            {
                var employee = _employeeService.GetById(leave.EmployeeId);
                Console.WriteLine($"Служител: {employee.Name}, Тип отпуск: {leave.LeaveType}, Начало: {leave.StartDate:yyyy-MM-dd}, Край: {leave.EndDate:yyyy-MM-dd}, Статус: {leave.Status}");
                //Console.WriteLine($"Employee: {employee.Name}, Leave Type: {leave.LeaveType}, Start: {leave.StartDate:yyyy-MM-dd}, End: {leave.EndDate:yyyy-MM-dd}, Status: {leave.Status}");
            }
            Console.ReadLine();
        }

        private void ApproveOrDenyVacantionRequsts()
        {
            Console.Clear();
            Console.WriteLine("--- Одобряване или отказване на заявка за отпуск ---");
            _leaveService.GetAllLeaves().Where(r => r.Status == LeaveStatus.Pending);
            Console.Write("Въведи id на заявка за отпуск: ");
            int leaveId = int.Parse(Console.ReadLine());
            Console.Write("Approve (Yes/No): ");
            string approveInput = Console.ReadLine();
            bool approve = approveInput.Equals("Yes", StringComparison.OrdinalIgnoreCase);
            try
            {
                if (approve)
                {
                    _leaveService.ApproveLeave(leaveId);
                    Console.WriteLine("Заявката за отпуск беше одобрена успешно!");
                }
                else
                {
                    _leaveService.RejectLeave(leaveId);
                    Console.WriteLine("Заявката за отпуск беше отказана успешно!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void CheckAvailableVacationDaysForEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Проверка за налични дни отпуск за служител ---");
            Console.Write("Въведете id на служител: ");
            int employeeId = int.Parse(Console.ReadLine());
            int usedDays = _leaveService.GetUsedLeaveDays(employeeId, DateTime.Now.Year);
            int remainingDays = 20 - usedDays;
            Console.WriteLine($"Служител с id {employeeId} има {remainingDays} оставащи дни платена отпуска за тази година.");
            Console.ReadLine();
        }

        private void VacationRequestByEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Подаване на заявка за отпуск от служител ---");
            Console.Write("Въведи id на служител: ");
            int employeeId = int.Parse(Console.ReadLine());
            Console.Write("Въведи начална дата (yyyy-MM-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Въведи крайна дата (yyyy-MM-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Въведи тип на отпуска (Vacation/Sick/Unpaid): ");
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
                    Console.WriteLine("Невалиден тип отпуск!");
                    return;
            }
            var leave = new Leave(employeeId, leaveType, startDate, endDate);
            try
            {
                _leaveService.RequestLeave(leave);
                Console.WriteLine("Успешно подадена заявка!");
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
            Console.Write("Въведете опция: ");

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
                    Console.WriteLine("Невалидна опция!");
                    break;
            } 
        }

        private void ShowEmployeesByDepartment()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на служители по отдел ---");
            Console.Write("Въведи id на отдел: ");
            int departmentId = int.Parse(Console.ReadLine());
            var employees = _employeeService.GetAllActiveEmployees()
                .Where(e => e.DepartmentId == departmentId)
                .ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("Няма намерени служители от въведения отдел.");
                return;
            }
            Console.WriteLine($"Служители в отдел {departmentId}:");
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
            Console.Write("Въведи id на позиция: ");
            int positionId = int.Parse(Console.ReadLine());
            var employees = _employeeService.GetAllActiveEmployees()
                .Where(e => e.PositionId == positionId)
                .ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("Няма намерени служители с въведената позиция.");
                return;
            }
            Console.WriteLine($"Служители с позиция {positionId}:");
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
            Console.Write("Въведи опция: ");

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
                    Console.WriteLine("Невалидна опция!");
                    break;
            }
        }
        private void AddPosition()
        {
            Console.WriteLine("\n--- Добавяне на нова позиция ---");

            var allDepartments = _departmentService.GetAllDepartments();
            Console.WriteLine("Налични отдели:");
            foreach (var d in allDepartments)
            {
                Console.WriteLine($"{d.DepartmentId} | {d.Name}");
            }

            Console.Write("За кой отдел е позицията (Department id): ");
            int departmentId = int.Parse(Console.ReadLine());
            _departmentService.GetById(departmentId);

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
                var position = new Position(title, description, baseSalary, departmentId);

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
                var allEmployees = _employeeService.GetAllActiveEmployees();
                Console.WriteLine("Активни служители");
                foreach(var e in allEmployees)
                {
                    Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title} | {e.Salary.Amount} euro");
                }
                Console.Write("Въведи id на служителя: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);

                Console.Write("Въведи нова заплата (euro): ");
                decimal newSalary = decimal.Parse(Console.ReadLine());
                //if (!decimal.TryParse(Console.ReadLine(), out decimal newFormatSalary))
                //{
                //    Console.WriteLine("Invalid salary format!");
                //    return;
                //}

                _employeeService.UpdateSalary(employeeId, newSalary);
                Console.WriteLine($"Заплатата на служител с id {employeeId} беше успешно променена!");

                //employee.Salary.Amount = newSalary;
                //_employeeService.UpdateEmployee(employee);
                //Console.WriteLine($"Заплатата на служител с id {employeeId} беше успешно променена!");
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
                var allEmployees = _employeeService.GetAllActiveEmployees();
                Console.WriteLine("Активни служители");
                foreach (var e in allEmployees)
                {
                    Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title} | {e.Salary.Amount} euro");
                }

                Console.Write("Въведи id на служител: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);

               
                Console.WriteLine($"Current Department: {_departmentService.GetById(employee.DepartmentId).Name}");

                var allDepartments = _departmentService.GetAllDepartments();
                Console.WriteLine("Available Departments:");
                foreach (var d in allDepartments)
                {
                    Console.WriteLine($"{d.DepartmentId} | {d.Name}");
                }
                Console.Write("Enter new Department id: ");
                int newDepartmentId = int.Parse(Console.ReadLine());
                _departmentService.GetById(newDepartmentId);

                Console.WriteLine($"Current Position: {_positionService.GetById(employee.PositionId).Title}");
                var allPositions = _positionService.GetAllPositions().Where(d => d.DepartmentId == newDepartmentId).ToList();
                if (allPositions.Count == 0)
                {
                    Console.WriteLine("Няма позиции за този отдел!");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Налични позиции в отделa:");
                foreach (var p in allPositions)
                {
                    Console.WriteLine($"{p.Id} | {p.Title}");
                }
                Console.Write("Enter new Position id: ");
                int newPositionId = int.Parse(Console.ReadLine());

                if (!allPositions.Select(p => p.Id).Contains(newPositionId))
                {
                    Console.WriteLine("Тази позиция не е от избрания отдел!");
                    Console.ReadLine();
                    return;
                }

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
                Console.WriteLine($"Служителят с {employeeId} id беше редактиран успешно!");
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

            if (employees.Count == 0)
            {
                Console.WriteLine("Няма активни служители.");
                Console.ReadLine();
                return;
            }

            //foreach (var employee in employees)
            //{
            //    string departmentName = employee.Department?.Name ?? "(няма отдел)";
            //    string positionTitle = employee.Position?.Title ?? "(няма позиция)";
            //    string salary = employee.Salary?.Amount.ToString() ?? "(няма заплата)";

            //    Console.WriteLine($"{employee.Id} | {departmentName} | {positionTitle} | {employee.Name} | {salary} лв. | {employee.Email}");
            //}

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Department.Name} | {employee.Name} | {employee.Salary.Amount} euro | {employee.Email}");
            }
            Console.ReadLine();
        }
    } 
}

