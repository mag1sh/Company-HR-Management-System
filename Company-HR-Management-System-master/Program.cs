using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.ConsoleUI;
using CompanyHRManagementSystem.Employees.Domain.Entities;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using CompanyHRManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CompanyHRManagementSystem
{
    //maj dobavih packetite ama nz
    internal class Program
    {

        static void Main(string[] args)
        {
            using (var context = new CompanyStorage())
            {
                context.Database.Migrate();
            }

            //CompanyStorage sharedStorage = new CompanyStorage();

            //var employeeRepo = new FileEmployeeRepository(sharedStorage);
            //var leaveRepo = new FileLeaveRepository(sharedStorage);

            //var employeeService = new EmployeeService(employeeRepo);
            //var leaveService = new LeaveService(leaveRepo, employeeRepo);
            //var departmentService = new DepartmentService(sharedStorage);
            //var positionService = new PositionService(sharedStorage);

            //var ui = new HRConsoleUI(employeeService, departmentService, positionService, leaveService);

            //ui.Start();

            //using (var context = new CompanyStorage())
            //{
            //    context.Database.Migrate();

            //    //var employeesWithoutSalary = context.Employees
            //    //    .Where(e => !context.Salaries.Any(s => s.EmployeeId == e.Id))
            //    //    .ToList();

            //    //foreach (var employee in employeesWithoutSalary)
            //    //{
            //    //    var position = context.Positions.FirstOrDefault(p => p.Id == employee.PositionId);
            //    //    if (position != null)
            //    //    {
            //    //        context.Salaries.Add(new Salary
            //    //        {
            //    //            EmployeeId = employee.Id,
            //    //            Amount = position.BaseSalary
            //    //        });
            //    //    }
            //    //}
            //    //context.SaveChanges();
            //}

            CompanyStorage sharedStorage = new CompanyStorage();

            var employeeRepo = new FileEmployeeRepository(sharedStorage);
            var leaveRepo = new FileLeaveRepository(sharedStorage);
            var departmentRepo = new FileDepartmentRepository(sharedStorage);
            var positionRepo = new PositionRepository(sharedStorage);

            var employeeService = new EmployeeService(employeeRepo);
            var leaveService = new LeaveService(leaveRepo, employeeRepo);
            var departmentService = new DepartmentService(departmentRepo);
            var positionService = new PositionService(positionRepo);

            var ui = new HRConsoleUI(employeeService, departmentService, positionService, leaveService);
            ui.Start();
        }
    }
}
