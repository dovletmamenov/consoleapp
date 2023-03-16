using ConsoleApp.DataModels;
using ConsoleApp.Interfaces;

namespace ConsoleApp.Services
{
    public class EmployeeValidator : IEmployeeValidator
    {
        private readonly ILogger _logger;
        public EmployeeValidator(ILogger loggerService)
        {
            _logger = loggerService;
        }
        public bool Validate(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                _logger.Log("Employee name cannot be empty.");
                return false;
            }

            if (employee.Salary < 1)
            {
                _logger.Log("Employee salary cannot be less than 1.");
                return false;
            }

            return true;
        }
    }
}
