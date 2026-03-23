namespace EmployeesScheduler.Contracts.DTOs.ScheduleDTOs;

public record CreateOverrideIntervalDto(
    DateTime StartDateTime,
    DateTime EndDateTime,
    int StateId);