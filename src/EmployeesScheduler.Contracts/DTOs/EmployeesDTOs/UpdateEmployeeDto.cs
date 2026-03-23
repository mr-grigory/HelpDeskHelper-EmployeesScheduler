namespace EmployeesScheduler.Contracts.DTOs.EmployeesDTOs;

public record UpdateEmployeeDto(string FirstName, 
    string LastName, 
    bool? Active);