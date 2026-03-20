using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Schedule;

/// <summary>
/// Дефолтный график сотрудника
/// </summary>
public class WorkSchedule
{
    private readonly List<CycleInterval> _intervals = [];

    public Guid Id { get; }
    public Guid EmployeeId { get; private set; }
    
    public DateTime StartDate { get; private set; }

    public IReadOnlyCollection<CycleInterval> Intervals => _intervals;

    // EF Core
    private WorkSchedule()
    {
    }

    private WorkSchedule(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate)
    {
        Id = Guid.NewGuid();
        _intervals = intervals.ToList();
        EmployeeId = employeeId;
        StartDate = DateTime.SpecifyKind(
            startDate.ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);
    }
    
    private static Result<List<CycleInterval>, Error> ValidateIntervals(ICollection<CycleInterval> intervals)
    {
        if (intervals.Count == 0)
            return Error.Validation("WorkSchedule.Create", 
                "Intervals cannot be empty", 
                field: "intervals");
        
        var items = intervals.OrderBy(x => x.OffsetFromCycleStart).ToList();
        
        var expectedStart = TimeSpan.Zero;
        
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
    
    /// <summary>
    /// Метод создания нового шаблона графика. Приоритет отдается графику с самой свежей startDate
    /// </summary>
    /// <param name="intervals">Расписание сотрудника</param>
    /// <param name="employeeId">Id сотрудника</param>
    /// <param name="startDate">Дата начала действия графика в UTC</param>
    /// <returns></returns>
    public static Result<WorkSchedule, Error> Create(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate)
    {
        // intervals validation
        var intervalValidateResult = ValidateIntervals(intervals);
        
        if (intervalValidateResult.IsFailure)
            return intervalValidateResult.Error;
        var validIntervals = intervalValidateResult.Value;
        
        return new WorkSchedule(validIntervals, employeeId, startDate);
    }

    /// <summary>
    /// Обновление графика без создания нового расписания.
    /// </summary>
    /// <param name="newIntervals">Новое расписание сотрудника</param>
    /// <returns></returns>
    public UnitResult<Error> ReplaceSchedule(ICollection<CycleInterval> newIntervals)
    {
        //TODO Добавить логику, что бы обновить график можно было только не позже даты его начала (или до его начала)
        
        // intervals validation
        var intervalValidateResult = ValidateIntervals(newIntervals);
        if (intervalValidateResult.IsFailure)
            return intervalValidateResult.Error;
        
        _intervals.Clear();
        _intervals.AddRange(intervalValidateResult.Value);
        
        return Result.Success<Error>();
    }

    public Result<WorkState, Error> GetStateAt(DateTime datetime)
    {
        var datetimeUtc = datetime.ToUniversalTime();

        if (datetimeUtc < StartDate)
        {
            return Error.Validation("WorkSchedule.GetStateAt", 
                $"The transmitted date {datetimeUtc} cannot be less than the initial date {StartDate}", 
                field: "datetime");
        }

        var maxSpan = _intervals.Last().OffsetFromCycleFinish;
        var difference = datetimeUtc - StartDate;
        var timeSpan = TimeSpan.FromTicks(difference.Ticks % maxSpan.Ticks);

        var resultInterval = _intervals.FirstOrDefault(x => x.Contains(timeSpan));

        if (resultInterval == null)
        {
            return Error.NotFound("WorkSchedule.GetStateAt", "not found TimeSpan in intervals");
        }

        return resultInterval.State;

    }
    
}
