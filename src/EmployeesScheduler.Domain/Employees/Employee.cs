using CSharpFunctionalExtensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Employees;

public class Employee
{
    public Guid Id { get; init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }
    
    private Employee(string firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Result<Employee, Error> Create(string firstName, string lastName)
    {
        if(string.IsNullOrWhiteSpace(firstName))
            return Error.Validation("Employee.Create", "First name cannot be empty.", field: "FirstName");
        if(string.IsNullOrWhiteSpace(lastName))
            return Error.Validation("Employee.Create", "Last name cannot be empty.", field: "LastName");
        
        return new Employee(firstName, lastName);
    }
    
    
}