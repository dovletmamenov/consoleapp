// See https://aka.ms/new-console-template for more information

using ConsoleApp.DataModels;
using ConsoleApp.Interfaces;
using ConsoleApp.Services;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();
var connectionString = config.GetConnectionString("DefaultConnection");
ILogger logger = new ConsoleLogger();
IEmployeeValidator employeeValidator = new EmployeeValidator(logger);
IEmployeeRepository employeeRepository = new MsSqlEmployeeRepository(employeeValidator, logger, connectionString);



Console.WriteLine("Please enter employee name:");
var newEmployeeName = Console.ReadLine();

var newEmployee = new Employee() { Name = newEmployeeName, Salary = 100 };
employeeRepository.AddEmployee(newEmployee);

var employeeList = employeeRepository.GetEmployees(DateTime.UtcNow);
foreach (var employee in employeeList)
{
    Console.WriteLine($"ID:{employee.ID} Name:{employee.Name}, Salary:{employee.Salary}");
}









