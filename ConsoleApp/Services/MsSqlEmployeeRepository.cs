using ConsoleApp.DataModels;
using ConsoleApp.Interfaces;
using Microsoft.Data.SqlClient;

namespace ConsoleApp.Services
{


    public class MsSqlEmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;
        private readonly IEmployeeValidator _validator;

        public MsSqlEmployeeRepository(IEmployeeValidator employeeValidator, ILogger logger, string connectionString)
        {
            _connectionString = connectionString;
            _validator = employeeValidator;
            _logger = logger;
        }

        public void AddEmployee(Employee employee)
        {
            if (!_validator.Validate(employee))
            {
                _logger.Log("Failed to add employee. Invalid employee data.");
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(@"INSERT INTO Employee (Name, Salary, ValidFrom, ValidTo)
                                       VALUES (@Name, @Salary, @ValidFrom, @ValidTo);
                                       SELECT SCOPE_IDENTITY()", connection);

                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@ValidFrom", DateTime.UtcNow);
                command.Parameters.AddWithValue("@ValidTo", DBNull.Value);

                int newEmployeeId = Convert.ToInt32(command.ExecuteScalar());

                employee.ID = newEmployeeId;

                _logger.Log($"Employee with ID {newEmployeeId} added successfully.");
            }
        }

        public IEnumerable<Employee> GetEmployees(DateTime date)
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(@"SELECT ID, Name, Salary, ValidFrom, ValidTo
                                            FROM Employee
                                           WHERE ValidFrom <= @Date
                                             AND (ValidTo >= @Date OR ValidTo IS NULL)", connection);

                command.Parameters.AddWithValue("@Date", date);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Salary = reader.GetDecimal(2)
                    };

                    employees.Add(employee);
                }
            }

            return employees;
        }
    }

}
