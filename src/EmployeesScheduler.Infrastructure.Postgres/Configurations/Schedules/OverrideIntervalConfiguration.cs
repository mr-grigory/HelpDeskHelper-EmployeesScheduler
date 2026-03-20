using EmployeesScheduler.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesScheduler.Infrastructure.Postgres.Configurations.Schedules;

public class OverrideIntervalConfiguration : IEntityTypeConfiguration<OverrideInterval>
{
    public void Configure(EntityTypeBuilder<OverrideInterval> builder)
    {
        
    }
}