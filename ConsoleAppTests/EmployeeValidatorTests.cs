using ConsoleApp.DataModels;
using ConsoleApp.Interfaces;
using ConsoleApp.Services;
using Moq;
using Xunit;

namespace EmployeeRepositoryTests
{


    public class EmployeeValidatorTests
    {
        [Fact]
        public void Validate_ShouldReturnTrue_WhenEmployeeIsValid()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var validator = new EmployeeValidator(loggerMock.Object);
            var employee = new Employee { Name = "John Doe", Salary = 1000 };

            // Act
            var result = validator.Validate(employee);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalseAndLog_WhenEmployeeNameIsEmpty()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var validator = new EmployeeValidator(loggerMock.Object);
            var employee = new Employee { Name = "", Salary = 1000 };

            // Act
            var result = validator.Validate(employee);

            // Assert
            Assert.False(result);
            loggerMock.Verify(l => l.Log("Employee name cannot be empty."), Times.Once);
        }

        [Fact]
        public void Validate_ShouldReturnFalseAndLog_WhenEmployeeSalaryIsLessThan1()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var validator = new EmployeeValidator(loggerMock.Object);
            var employee = new Employee { Name = "John Doe", Salary = 0 };

            // Act
            var result = validator.Validate(employee);

            // Assert
            Assert.False(result);
            loggerMock.Verify(l => l.Log("Employee salary cannot be less than 1."), Times.Once);
        }
    }

}
