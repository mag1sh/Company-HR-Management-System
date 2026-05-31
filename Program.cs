using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Application;
using CompanyHRManagementSystem.Employees.ConsoleUI;
using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


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

            CompanyStorage sharedStorage = new CompanyStorage();
 
            var employeeService = new EmployeeService(sharedStorage);
            var leaveService = new LeaveService(sharedStorage);
            var departmentService = new DepartmentService(sharedStorage);
            var positionService = new PositionService(sharedStorage);

            var ui = new HRConsoleUI(employeeService, departmentService, positionService, leaveService);

            ui.Start();
        }
    }
}
