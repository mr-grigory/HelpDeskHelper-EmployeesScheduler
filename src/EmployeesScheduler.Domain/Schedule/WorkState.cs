namespace EmployeesScheduler.Domain.Schedule;

/// <summary>
/// Типы промежутка времени. Можно учитывать при аналитике отпусков, отгулов. Так же обеденнех перерывов и тд
/// </summary>
public enum WorkState
{
    // Рабочаяя смена
    Work,
    
    // Выходной
    DayOff,
}
