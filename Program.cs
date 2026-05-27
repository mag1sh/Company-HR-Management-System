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


namespace CompanyHRManagementSystem
{
    //maj dobavih packetite ama nz
    internal class Program
    {
        static void Main(string[] args)
        {
            
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
