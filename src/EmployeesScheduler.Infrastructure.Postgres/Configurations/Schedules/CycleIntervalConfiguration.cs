using EmployeesScheduler.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesScheduler.Infrastructure.Postgres.Configurations.Schedules;

public class CycleIntervalConfiguration : IEntityTypeConfiguration<CycleInterval>
{
    public void Configure(EntityTypeBuilder<CycleInterval> builder)
    {
        
    }
}