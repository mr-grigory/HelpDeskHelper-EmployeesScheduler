using CSharpFunctionalExtensions;
using EmployeesScheduler.Domain.Extensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Schedule;

/// <summary>
/// Отрезок времени в расписании
/// </summary>
public class ScheduleInterval
{
    public TimeSpan OffsetFromCycleStart { get; }
    public TimeSpan Duration { get; }
    public TimeSpan OffsetFromCycleFinish => OffsetFromCycleStart + Duration;
    public WorkState State { get; }

    // EF Core
    private ScheduleInterval()
    {
    }

    private  ScheduleInterval(TimeSpan offsetFromCycleStart, TimeSpan duration, WorkState state)
    {
        OffsetFromCycleStart = offsetFromCycleStart;
        Duration = duration;
        State = state;
    }

    /// <summary>
    /// Метод для создания экземпляра класса
    /// </summary>
    /// <param name="offsetFromCycleStart">Отступ от начальной точки расписания</param>
    /// <param name="duration">Продолжительность интервала</param>
    /// <param name="state">Поведение расписания в этот интервал времени</param>
    /// <returns></returns>
    public static Result<ScheduleInterval, Error> Create(TimeSpan offsetFromCycleStart, TimeSpan duration, WorkState state)
    {
        if (offsetFromCycleStart < TimeSpan.Zero)
        {
            return Error.Validation(
                "CycleInterval.Create", 
                "offsetFromCycleStart must be greater than or equal to zero.");
        }

        if (duration <= TimeSpan.Zero)
        {
            return Error.Validation(
                "CycleInterval.Create",
                "duration must be greater than to zero.");
        }
            
        return new ScheduleInterval(offsetFromCycleStart, duration, state);
    }

    /// <summary>
    /// Проверка, что точка времени входит в интервал
    /// </summary>
    /// <param name="time">Конкретный момент времени</param>
    /// <returns>true - timespan входит в этот интервал, false - не входит</returns>
    public bool Contains(TimeSpan time)
    {
        return OffsetFromCycleStart <= time && time < OffsetFromCycleFinish;
    }

    /// <summary>
    /// Проверка наличия пересечения двух интервалов
    /// </summary>
    /// <param name="otherInterval">Второй интервал, с которым проверить</param>
    /// <returns>true - если найдены пересечения, false - интервалы не пересекаются</returns>
    public bool Intersects(ScheduleInterval otherInterval)
    {
        return this.OffsetFromCycleStart < otherInterval.OffsetFromCycleFinish &&
               otherInterval.OffsetFromCycleStart < this.OffsetFromCycleFinish;
    }
}
