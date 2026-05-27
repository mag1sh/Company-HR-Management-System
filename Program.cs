using CompanyHRManagementSystem.Employees.Infrastructure;
using CompanyHRManagementSystem.Employees.Services.Interfaces;
using CompanyHRManagementSystem.Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyHRManagementSystem.Employees.ConsoleUI;


namespace CompanyHRManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeService employeeService = new EmployeeService() ;
            HRConsoleUI ui = new HRConsoleUI(employeeService);
            ui.Start();
        }
    }
}
