namespace EmployeesSheduler.Domain.Shedule;

public class CycleInterval
{
    public TimeSpan OffsetFromCycleStart { get; }
    public TimeSpan Duration  { get; }
    
    public WorkState State  { get; }
}