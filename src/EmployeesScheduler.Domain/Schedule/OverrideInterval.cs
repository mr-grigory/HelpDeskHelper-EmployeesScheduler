using CSharpFunctionalExtensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Schedule;

/// <summary>
/// Переопределяет график по умолчанию
/// </summary>
public class OverrideInterval
{
    public Guid Id { get; }
    public Guid EmployeeId { get; }
    public DateTime  StartAt { get; init; }
    public DateTime EndAt { get; init; }
    public WorkState State { get; init; }
    
    public bool AppliesTo(DateTime datetime) => datetime >= StartAt && datetime <= EndAt;
    
    private OverrideInterval(Guid employeeId, DateTime startAt, DateTime endAt, WorkState state)
    {
        Id = Guid.NewGuid();
        EmployeeId = employeeId;
        StartAt = startAt;
        EndAt = endAt;
        State = state;
    }

    /// <summary>
    /// Создает новый экземпляр переопределенного интервала в графике сотруднике.
    /// </summary>
    /// <param name="employeeId">Id сотрудника</param>
    /// <param name="startAt">Дата и время начала действия</param>
    /// <param name="endAt">Дата и время окончания действия</param>
    /// <param name="state">Состояние в указанный период</param>
    /// <returns>Возвращает экземпляр данных или ошибку</returns>
    public static Result<OverrideInterval, Error> Create(Guid employeeId, DateTime startAt, DateTime endAt, WorkState state)
    {
        if (startAt >= endAt)
            return Error.Validation("OverrideInterval.Create", "startAt cannot be greater than endAt");
        
        return new OverrideInterval(employeeId, startAt, endAt, state);
    }
}