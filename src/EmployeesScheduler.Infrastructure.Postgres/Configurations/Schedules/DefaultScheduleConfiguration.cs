using EmployeesScheduler.Domain.Employees;
using EmployeesScheduler.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesScheduler.Infrastructure.Postgres.Configurations.Schedules;

public class DefaultScheduleConfiguration : IEntityTypeConfiguration<DefaultSchedule>
{
    public void Configure(EntityTypeBuilder<DefaultSchedule> builder)
    {
        builder.ToTable("work_schedules");
        
        builder.HasKey(ws => ws.Id).HasName("pk_work_schedules");

        builder.Property(ws => ws.Id)
            .HasColumnName("id");
        
        builder.Property(ws => ws.EmployeeId)
            .HasColumnName("employee_id")
            .IsRequired();

        builder.Property(ws => ws.StartDate)
            .HasColumnName("start_date")
            .IsRequired();


        builder.OwnsMany(ws => ws.Intervals, intervals =>
        {
            intervals.ToTable("schedule_intervals");
            
            intervals.WithOwner()
                .HasForeignKey("work_schedule_id");

            intervals.Property<Guid>("id");
            intervals.HasKey("id");
            
            
            intervals.Property(i => i.OffsetFromCycleStart)
                .HasColumnName("offset_from_start")
                .IsRequired();
            
            intervals.Property(i => i.OffsetFromCycleFinish)
                .HasColumnName("offset_from_finish")
                .IsRequired();
            
            intervals.Property(i => i.Duration)
                .HasColumnName("duration")
                .IsRequired();
            
            intervals.Property(i => i.State)
                .HasColumnName("state")
                .IsRequired();
        });
        
        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(ws => ws.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}