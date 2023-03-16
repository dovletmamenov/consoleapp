using ConsoleApp.DataModels;

namespace ConsoleApp.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        IEnumerable<Employee> GetEmployees(DateTime date);
    }
}
