using CSharpFunctionalExtensions;
using MrGrigory.MyUtils;

namespace EmployeesScheduler.Domain.Schedule;

public class WorkSchedule
{
    private WorkSchedule(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate, TimeSpan cycleLength)
    {
        Id = Guid.NewGuid();
        _intervals = intervals.ToList();
        EmployeeId = employeeId;
        StartDate = startDate;
        CycleLength = cycleLength;
    }
    
    private readonly List<CycleInterval> _intervals;

    public Guid Id { get; }
    public Guid EmployeeId { get; private set; }
    
    public DateOnly StartDate { get; private set; }
    public TimeSpan CycleLength { get; private set; }

    public IReadOnlyCollection<CycleInterval> Intervals => _intervals;


    public static Result<WorkSchedule, Error> Create(ICollection<CycleInterval> intervals, Guid employeeId, DateOnly startDate, TimeSpan cycleLength)
    {
        //TODO проверить что длина CycleLength равна длине intervals 
        //THINK а нужно ли принимать на входе CycleLength или считать ее исходя из массива?
        
        //TODO проверить, что в CycleLength не пересекаются диапозоны
        
        return new WorkSchedule(intervals, employeeId, startDate, cycleLength);
    }
}
