namespace EmployeesScheduler.Contracts.DTOs.ScheduleDTOs;

public record UpdateDefaultScheduleDto(
    DateTime StartDateTime,
    CreateIntervalDto[] Intervals);