namespace EmployeesSheduler.Domain.Shedule;

public class WorkSchedule
{
    public Guid Id { get; private set; }
    public Guid EmployeeId { get; private set; }
    
    public DateTime StartDate { get; private set; }
    public TimeSpan CycleLengt { get; private set; }

    public IReadOnlyCollection<CycleInterval> Intervals { get; private set; }
}