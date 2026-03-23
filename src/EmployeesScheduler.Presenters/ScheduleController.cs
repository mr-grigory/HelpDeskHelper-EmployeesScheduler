using EmployeesScheduler.Contracts.DTOs.ScheduleDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesScheduler.Presenters;

[ApiController]
[Route("api/[controller]/{employeeId:guid}")]
public class ScheduleController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetRange(
        [FromRoute] Guid employeeId,
        [FromQuery] DateTime? startDateTime,
        [FromQuery] DateTime? endDateTime,
        CancellationToken cancellationToken)
    {
        return Ok("schedule retrieved");
    }

    [HttpPut("default")]
    public async Task<IActionResult> CreateSchedule(
        [FromRoute] Guid employeeId,
        [FromBody] UpdateDefaultScheduleDto request,
        CancellationToken cancellationToken)
    {
        return Ok("schedule updated");
    }

    [HttpPut("override")]
    public async Task<IActionResult> UpdateInterval(
        [FromRoute] Guid employeeId,
        [FromBody] CreateOverrideIntervalDto[] request,
        CancellationToken cancellationToken)
    {
        return Ok("interval updated");
    }
}