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
     }
    
}
