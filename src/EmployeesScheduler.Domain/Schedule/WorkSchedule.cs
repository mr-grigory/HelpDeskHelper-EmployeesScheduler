using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Schedule;

public class WorkSchedule
{
    private WorkSchedule(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate)
    {
        Id = Guid.NewGuid();
        _intervals = intervals.ToList();
        EmployeeId = employeeId;
        StartDate = startDate;
    }
    
    private readonly List<CycleInterval> _intervals;

    public Guid Id { get; }
    public Guid EmployeeId { get; private set; }
    
    public DateOnly StartDate { get; private set; }

    public IReadOnlyCollection<CycleInterval> Intervals => _intervals;


    public static Result<WorkSchedule, Error> Create(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate)
    {
        // intervals validation
        var intervalValidateResult = ValidateIntervals(intervals);
        if (intervalValidateResult.IsFailure)
            return intervalValidateResult.Error;
        var validIntervals = intervalValidateResult.Value;
        
        return new WorkSchedule(validIntervals, employeeId, startDate);
    }

    private static Result<List<CycleInterval>, Error> ValidateIntervals(ICollection<CycleInterval> intervals)
    {
        if (intervals.Count == 0)
            return Error.Validation("WorkSchedule.Create", 
                "Intervals cannot be empty", 
                field: "intervals");
        
        var items = intervals.OrderBy(x => x.OffsetFromCycleStart).ToList();
        
        TimeSpan expectedStart = TimeSpan.Zero;
        
        foreach (var interval in items)
        {
            if (interval.OffsetFromCycleStart != expectedStart)
            {
                return Error.Validation("WorkSchedule.Create",
                    $"Gap or overlap detected. Expected start: {expectedStart}, actual: {interval.OffsetFromCycleStart}", 
                    field: "intervals");
            }

            expectedStart = interval.OffsetFromCycleFinish;
        }

        return items;
    }
    
    
    
}
