using EmployeesScheduler.Contracts.DTOs.EmployeesDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesScheduler.Presenters;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeDto request, 
        CancellationToken cancellationToken)
    {
        return Ok("user created");
    }
    
    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid employeeId, 
        CancellationToken cancellationToken)
    {
        return Ok("user retrieved");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] bool includeInactive = false)
    {
        return Ok("users retrieved");
    }

    [HttpPut("{employeeId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid employeeId,
        [FromBody] UpdateEmployeeDto request,
        CancellationToken cancellationToken)
    {
        return Ok("user updated");
    }
}