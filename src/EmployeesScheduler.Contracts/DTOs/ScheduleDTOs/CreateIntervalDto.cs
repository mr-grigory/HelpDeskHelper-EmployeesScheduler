namespace EmployeesScheduler.Contracts.DTOs.ScheduleDTOs;

public record CreateIntervalDto(
    TimeSpan OffsetFromStart,
    TimeSpan Duration,
    int StateId);