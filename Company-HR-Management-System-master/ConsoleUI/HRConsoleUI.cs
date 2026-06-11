using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Domain.Enums;
using CompanyHRManagementSystem.Employees.Domain.ValueObjects;
using CompanyHRManagementSystem.Employees.Services;
using Domain.Entities;
using System;
using System.Linq;
using System.Net;

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
                Console.WriteLine("10. Генериране на справка за служители по отдел или позиция");
                Console.WriteLine("11. Генериране на справка за отпуски по период");
                Console.WriteLine("12. Генериране за справка за текучество на персонала");
                Console.WriteLine("13. Проследяване на трудовия стаж на служител от компанията");
                Console.WriteLine("14. История за кадровите промени за конкретен служител");
                Console.WriteLine("15. Показване на историята на заплатите на служител");
                Console.WriteLine("16. Разширена справка за служители по условия");
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
                        break;
                    case "7":
                        CheckAvailableVacationDaysForEmployee();
                        break;
                    case "8":
                        ApproveOrDenyVacantionRequsts();
                        break;
                    case "9":
                        CheckForVacationConflictsInDepartment();
                        break;
                    case "10":
                        ShowEmployeesByDepartmentOrPosition();
                        break;
                    case "11":
                        ShowLeaveRequestsByPeriod();
                        break;
                    case "12":
                        GenerateEmployeeTurnoverReport();
                        break;
                    case "13":
                        TrackStajNaEmployee();
                        break;
                    case "14":
                        ShowEmploymentHistory();
                        break;
                    case "15":
                        ShowSalaryHistory();
                        break;
                    case "16":
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
            try
            {
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
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Служители с заплата между {minSalary} и {maxSalary}, от отдел {(departmentId.HasValue ? departmentId.ToString() : "всички")} и позиция {(positionId.HasValue ? positionId.ToString() : "всички")}:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.Id} | {employee.Department.Name} | {employee.Position.Title} | {employee.Name} | {employee.Salary.Amount} | {employee.Email}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ShowSalaryHistory()
        {
            Console.Clear();
            Console.WriteLine("--- История на заплатите на служител ---");

            try
            {
                var allEmployees = _employeeService.GetAllActiveEmployees();
                foreach (var e in allEmployees)
                {
                    Console.WriteLine($"{e.Id} | {e.Name} | {e.Salary.Amount} euro");
                }

                Console.Write("Въведете id на служител: ");
                int employeeId = int.Parse(Console.ReadLine());

                var salaryHistory = _employeeService.GetSalaryHistory(employeeId);
                if (salaryHistory.Count == 0)
                {
                    Console.WriteLine("Няма намерени записи за заплатите на този служител.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"История на заплатите за служител с id {employeeId}:");
                foreach (var record in salaryHistory)
                {
                    Console.WriteLine($"{record.ChangeDate:yyyy-MM-dd} | {record.OldSalary} -> {record.NewSalary} | {record.Reason}");
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ShowEmploymentHistory()
        {
            Console.Clear();
            Console.WriteLine("--- История за кадровите промени за конкретен служител ---");
            try
            {
                var allEmployees = _employeeService.GetAllActiveEmployees();
                foreach (var e in allEmployees)
                {
                    Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title}");
                }

                Console.Write("Въведете id на служител: ");
                int employeeId = int.Parse(Console.ReadLine());

                var history = _employeeService.GetEmploymentHistory(employeeId);
                if (history.Count == 0)
                {
                    Console.WriteLine("Няма намерени кадрови промени за този служител.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"Кадрови промени за служител с id {employeeId}:");
                foreach (var change in history)
                {
                    Console.WriteLine($"{change.ChangeDate:yyyy-MM-dd} | Отдел: {change.OldDepartment} -> {change.NewDepartment} | Позиция: {change.OldPosition} -> {change.NewPosition}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void TrackStajNaEmployee()
        {
            Console.WriteLine("--- Проследяване на трудовия стаж на служител от компанията ---");

            var allEmployees = _employeeService.GetAllEmployees();
            foreach (var e in allEmployees)
            {
                Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title} || {e.Status}");
            }

            Console.Write("Въведете id на служител: ");
            try
            {
                int employeeId = int.Parse(Console.ReadLine());

                Employee employee = _employeeService.GetById(employeeId);
                if (employee == null)
                {
                    Console.WriteLine("Служителят с въведения id не съществува.");
                    return;
                }

                TimeSpan staj = (employee.TerminationDate ?? DateTime.Now) - employee.HireDate;
                Console.WriteLine($"Служител {employee.Name} има {staj.Days / 365} години, {(staj.Days % 365) / 30} месеца и {(staj.Days % 365) % 30} дни трудов стаж в компанията.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
            }
            Console.ReadLine();
        }

        private void GenerateEmployeeTurnoverReport()
        {
            Console.Clear();
            Console.WriteLine("--- Справка за текучество на персонала ---");
            var allEmployees = _employeeService.GetAllEmployees();
            if (allEmployees.Count == 0)
            {
                Console.WriteLine("");
                Console.ReadLine();
                return;
            }

            var years = allEmployees.Select(e => e.HireDate.Year).Distinct().OrderBy(y => y).ToList();

            Console.WriteLine($"{"Година",-10} | {"Наети",-10} | {"Напуснали",-10} | {"Текучество %",-10}");
            Console.WriteLine(new string('-', 50));

            foreach (var year in years)
            {
                var group = allEmployees.Where(e => e.HireDate.Year == year).ToList();
                int hired = group.Count;
                int left = allEmployees.Count(e => e.TerminationDate.HasValue && e.TerminationDate.Value.Year == year);
                double turnover = hired == 0 ? 0 : (double)left / hired * 100;

                Console.WriteLine($"{year,-10} | {hired,-10} | {left,-10} | {turnover:F2}%");
            }
            Console.ReadLine();
        }

        private void CheckForVacationConflictsInDepartment()
        {
            Console.Clear();
            Console.WriteLine("--- Проверка за конфликт на отпуски в отдел ---");

            try
            {
                var allDepartments = _departmentService.GetAllDepartments();
                Console.WriteLine("Налични отдели:");
                foreach (var d in allDepartments)
                {
                    Console.WriteLine($"{d.DepartmentId} | {d.Name}");
                }
                Console.Write("Въведи id на отдел: ");
                int departmentId = int.Parse(Console.ReadLine());
                var conflicts = _leaveService.GetVacationConflictsInDepartment(departmentId).ToList();
                if (conflicts.Count == 0)
                {
                    Console.WriteLine("Няма конфликти на отпуски в този отдел.");
                }
                else
                {
                    Console.WriteLine("Конфликти на отпуски в отдела:");
                    foreach (var conflict in conflicts)
                    {
                        var employee1 = _employeeService.GetById(conflict.Leave1.EmployeeId);
                        var employee2 = _employeeService.GetById(conflict.Leave2.EmployeeId);
                        Console.WriteLine($"Конфликт между {employee1.Name} и {employee2.Name} | {conflict.Leave1.StartDate:yyyy-MM-dd} - {conflict.Leave1.EndDate:yyyy-MM-dd} и {conflict.Leave2.StartDate:yyyy-MM-dd} - {conflict.Leave2.EndDate:yyyy-MM-dd}");
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
            }
            Console.ReadLine();
        }

        private void ShowLeaveRequestsByPeriod()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на заявки за отпуск по период ---");

            try
            {
                Console.Write("Въведи начална дата (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Въведи крайна дата (yyyy-MM-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                var leaves = _leaveService.GetLeavesByPeriod(startDate, endDate);
                if (leaves.Count == 0)
                {
                    Console.WriteLine("Няма намерени заявки за отпуск за посочения период.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Заявки за отпуск от {startDate:yyyy-MM-dd} до {endDate:yyyy-MM-dd}:");
                foreach (var leave in leaves)
                {
                    var employee = _employeeService.GetById(leave.EmployeeId);
                    Console.WriteLine($"Служител: {employee.Name}, Тип отпуск: {leave.LeaveType}, Начало: {leave.StartDate:yyyy-MM-dd}, Край: {leave.EndDate:yyyy-MM-dd}, Статус: {leave.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
            }
            Console.ReadLine();
        }

        private void ApproveOrDenyVacantionRequsts()
        {
            Console.Clear();
            Console.WriteLine("--- Одобряване или отказване на заявка за отпуск ---");
            var pendingLeaves = _leaveService.GetAllLeaves()
                                             .Where(r => r.Status == LeaveStatus.Pending)
                                             .ToList();

            if (pendingLeaves.Count == 0)
            {
                Console.WriteLine("Няма чакащи заявки за отпуск.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Чакащи заявки:");
            foreach (var leave in pendingLeaves)
            {
                var employee = _employeeService.GetById(leave.EmployeeId);
                int remainingDays = _leaveService.GetRemainingLeaveDays(leave.EmployeeId, DateTime.Now.Year);
                Console.WriteLine($"{leave.Id} | {employee.Name} | {leave.LeaveType} | {leave.StartDate:yyyy-MM-dd} - {leave.EndDate:yyyy-MM-dd} | {leave.DaysCount} дни | Налични дни: {remainingDays} дни");
            }

            Console.Write("Въведи id на заявка за отпуск: ");    
            if (!int.TryParse(Console.ReadLine(), out int leaveId))
            {
                Console.WriteLine("Невалиден формат за id на заявка!");
                Console.ReadLine();
                return;
            }

            var leaveRequest = _leaveService.GetLeaveById(leaveId);
            if (leaveId == 0 || leaveRequest.Status != LeaveStatus.Pending)
            {
                Console.WriteLine("Невалидна заявка за отпуск!");
                Console.ReadLine();
                return;
            }

            var selectedLeave = _leaveService.GetLeaveById(leaveId);
            var selectedEmployee = _employeeService.GetById(selectedLeave.EmployeeId);
            bool hasConflict = _leaveService.HasConflictInDepartment(selectedLeave, selectedEmployee.DepartmentId);
            if (hasConflict)
            {
                Console.WriteLine("Открит е конфликт. При одобряване заявката ще бъде автоматично изтрита.");
            }

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
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void CheckAvailableVacationDaysForEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Проверка за налични дни отпуск за служител ---");

            var allEmployees = _employeeService.GetAllActiveEmployees();
            Console.WriteLine("Активни служители:");
            foreach (var e in allEmployees)
            {
                Console.WriteLine($"{e.Id} | {e.Name}");
            }

            Console.Write("Въведете id на служител: ");
            try
            {
                int employeeId = int.Parse(Console.ReadLine());
                int usedDays = _leaveService.GetUsedLeaveDays(employeeId, DateTime.Now.Year);
                int remainingDays = 20 - usedDays;
                Console.WriteLine($"Служител с id {employeeId} има {remainingDays} оставащи дни платена отпуска за тази година.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
            }
            Console.ReadLine();
        }

        private void VacationRequestByEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Подаване на заявка за отпуск от служител ---");

            var allEmployees = _employeeService.GetAllActiveEmployees();
            Console.WriteLine("Активни служители:");
            foreach (var e in allEmployees)
            {
                Console.WriteLine($"{e.Id} || {e.Name}");
            }
            Console.Write("Въведи id на служител: ");

            try
            {
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);
                if (employee.Status != EmployeeStatus.Active)
                {
                    Console.WriteLine("Служителят не е активен!");
                    Console.ReadLine();
                    return;
                }
                Console.Write("Въведи начална дата (yyyy-MM-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Въведи крайна дата (yyyy-MM-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());
                
                TimeSpan duration = endDate - startDate;
                if (duration.TotalDays < 1)
                {
                    Console.WriteLine("Крайна дата трябва да бъде след началната дата!");
                    Console.ReadLine();
                    return;
                }
                int remainingDays = 20 - _leaveService.GetUsedLeaveDays(employeeId, DateTime.Now.Year);
                if (remainingDays < duration.Days)
                {
                    Console.WriteLine("Нямате достатъчно дни!");
                    Console.ReadLine();
                    return;
                }

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

                _leaveService.RequestLeave(leave);
                Console.WriteLine("Успешно подадена заявка!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
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

            var allDepartments = _departmentService.GetAllDepartments();
            Console.WriteLine("Налични отдели:");
            foreach (var d in allDepartments)
            {
                Console.WriteLine($"{d.DepartmentId} | {d.Name}");
            }

            Console.Write("Въведи id на отдел: ");
            try
            {
                int departmentId = int.Parse(Console.ReadLine());

                var department = _departmentService.GetById(departmentId);
                if (department == null)
                {
                    Console.WriteLine("Невалиден id на отдел!");
                    Console.ReadLine();
                    return; //tuka malko ne ostava da pokaje che nqma takuv otdel nz shto
                }

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
            }
            Console.ReadLine();
        }

        private void ShowEmployeesByPosition()
        {
            Console.Clear();
            Console.WriteLine("--- Показване на служители по позиция ---");

            var allPostions = _positionService.GetAllPositions();
            Console.WriteLine("Налични позиции:");
            foreach (var p in allPostions)
            {
                Console.WriteLine($"{p.Id} | {p.Title}");
            }

            Console.Write("Въведи id на позиция: ");
            try
            {
                int positionId = int.Parse(Console.ReadLine());

                var position = _positionService.GetById(positionId);
                if(position == null)
                {
                    Console.WriteLine("Невалиден id на позиция!");
                    Console.ReadLine();
                    return; //tuka malko ne ostava da pokaje che nqma takava poziciq nz shto
                }

                var employees = _employeeService.GetAllActiveEmployees()
                    .Where(e => e.PositionId == positionId)
                    .ToList();
                if (employees.Count == 0)
                {
                    Console.WriteLine("Няма намерени служители с въведената позиция.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Служители с позиция {positionId}:");
                foreach (var employee in employees)
                {
                    Console.WriteLine($"{employee.Id} | {employee.Name} | {employee.Email} | {employee.PhoneNumber.Number}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
                return;
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
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty");
                Console.ReadLine();
                return;
            }

            Console.Write("Position Description: ");
            string description = Console.ReadLine();

            Console.Write("Base Salary for this position (euro): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalary))
            {
                Console.WriteLine("Invalid salary format! Position creation aborted.");
                Console.ReadLine();
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
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void ChangeEmployeeSalary()
        {
            try
            {
                var allEmployees = _employeeService.GetAllActiveEmployees();
                Console.WriteLine("Активни служители");
                foreach (var e in allEmployees)
                {
                    Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title} | {e.Salary.Amount} euro");
                }
                Console.Write("Въведи id на служителя: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);
                if (employee.Status != EmployeeStatus.Active)
                {
                    Console.WriteLine("Служителят не е активен!");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Въведи нова заплата (euro): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newSalary))
                {
                    Console.WriteLine("Invalid salary format!");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Въведи причина за промяната: ");
                string reason = Console.ReadLine();

                _employeeService.UpdateSalary(employeeId, newSalary, reason);
                Console.WriteLine($"Заплатата на служител с id {employeeId} беше успешно променена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
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
                if(employee.Status != EmployeeStatus.Active)
{
                    Console.WriteLine("Служителят не е активен!");
                    Console.ReadLine();
                    return;
                }

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
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void EditEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Редактиране на служител ---");
            var allEmployees = _employeeService.GetAllActiveEmployees();
            Console.WriteLine("Активни служители:");
            foreach (var e in allEmployees)
            {
                Console.WriteLine($"{e.Id} || {e.Name} || {e.Department.DepartmentId} - {e.Department.Name} || {e.PositionId} - {e.Position.Title} || {e.Email} || {e.PhoneNumber.Number} || {e.Address}");
            }
            try
            {
                Console.Write("Enter Employee id: ");
                int employeeId = int.Parse(Console.ReadLine());
                Employee employee = _employeeService.GetById(employeeId);
                if (employee.Status != EmployeeStatus.Active)
                {
                    Console.WriteLine("Служителят не е активен!");
                    Console.ReadLine();
                    return;
                }

                _employeeService.GetById(employeeId);

                Employee updatedemployee = GetEmployeInfo();
                updatedemployee.Id = employeeId;

                _employeeService.UpdateEmployee(updatedemployee);
                Console.WriteLine($"Служителят с {employeeId} id беше редактиран успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
        }

        private void DeactivateOrFireEmployee()
        {
            Console.Clear();
            Console.WriteLine("--- Деактивиране на служител ---");

            var allEmployees = _employeeService.GetAllActiveEmployees();
            Console.WriteLine("Активни служители:");
            foreach (var e in allEmployees)
            {
                Console.WriteLine($"{e.Id} | {e.Name} | {e.Department.Name} | {e.Position.Title}");
            }

            Console.Write("Въведи id на служител: ");
            try
            {
                int employeeId = int.Parse(Console.ReadLine());
                _employeeService.DeactivateEmployee(employeeId);
                Console.WriteLine($"Служителят с {employeeId} id беше деактивиран успешно!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");
            }
            Console.ReadLine();
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
            Console.Clear();
            Console.Write("Enter first Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter last Name: ");
            string lastName = Console.ReadLine();

            int deparmentId = 0;
            var alldepartments = _departmentService.GetAllDepartments().ToList();

            while (true)
            {
                foreach (var dep in alldepartments)
                {
                    Console.WriteLine($"{dep.DepartmentId} - {dep.Name}");
                }
                Console.Write("Choose department id: ");
                if (int.TryParse(Console.ReadLine(), out deparmentId))
                {
                    var department = _departmentService.GetById(deparmentId);
                    if (department != null && alldepartments.Any(d => d.DepartmentId == deparmentId))
                    {
                        break;
                    }
                }
                Console.WriteLine("Няма отдел с избраното id! Опитайте пак.\n");
            }

            int positionId = 0;
            var positionsInDep = _positionService.GetByDepartmentId(deparmentId).ToList();

            while (true)
            {
                foreach (var pos in positionsInDep)
                {
                    Console.WriteLine($"{pos.Id} - {pos.Title}");
                }
                Console.Write("Choose position id: ");
                if (int.TryParse(Console.ReadLine(), out positionId))
                {
                    var position = _positionService.GetById(positionId);
                    if (position != null && positionsInDep.Any(p => p.Id == positionId))
                    {
                        break;
                    }
                }
                Console.WriteLine("Няма позиция с това id или не е в избрания отдел! Опитайте пак.\n");
            }

            Console.Write("Enter email: ");
            string emailInput = Console.ReadLine();

            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter country: ");
            string country = Console.ReadLine();

            Console.Write("Enter city: ");
            string city = Console.ReadLine();

            Console.Write("Enter postal code: ");
            string postalCode = Console.ReadLine();

            Console.Write("Enter street: ");
            string street = Console.ReadLine();

            Console.Write("Enter street number: ");
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
                positionId
            );

            var employeesPosition = _positionService.GetById(positionId);
            employee.Salary = new Salary { Amount = employeesPosition.BaseSalary };

            return employee;
        }

        private void AddEmployee()
        {
            try
            {
                Employee employee = GetEmployeInfo();

                _employeeService.AddEmployee(employee);

                Console.WriteLine("Employee added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Грешка: {ex.Message}");

                // ТОВА ЩЕ НИ КАЖЕ ИСТИНАТА:
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Детайли от SQL: {ex.InnerException.Message}");

                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"Дълбоки детайли: {ex.InnerException.InnerException.Message}");
                    }
                }
            }
            Console.ReadLine();
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

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.Id} | {employee.Department.Name} | {employee.Position.Title} | {employee.Name} | {employee.Salary.Amount} euro | {employee.Email}");
            }
            Console.ReadLine();
        }
    }
}