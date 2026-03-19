using CSharpFunctionalExtensions;
using EmployeesScheduler.Domain.Schedule;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Extensions;

public static class WorkStateExtensions
{
    public static Result<int, Error> GetPriority(this WorkState state)
    {
        return state switch
        {
            WorkState.Work => 10,
            WorkState.DayOff => 10,
            _ => Error.Failure("WorkStateExtensions.GetPriority", 
                $"Not have priority for WorkState = {state}")
        };
    }
}