using EmployeesScheduler.Domain.Employees;
using EmployeesScheduler.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesScheduler.Infrastructure.Postgres.Configurations.Schedules;

public class OverrideIntervalConfiguration : IEntityTypeConfiguration<OverrideInterval>
{
    public void Configure(EntityTypeBuilder<OverrideInterval> builder)
    {
        builder.ToTable("override_intervals");
        builder.HasKey(o => o.Id).HasName("pk_override_intervals");

        builder.Property(o => o.Id).HasColumnName("id");
        
        builder.Property(o => o.EmployeeId)
            .HasColumnName("employee_id")
            .IsRequired();
        
        builder.Property(o => o.StartAt)
            .HasColumnName("start_at")
            .IsRequired();
        
        builder.Property(o => o.EndAt)
            .HasColumnName("end_at")
            .IsRequired();
        
        builder.Property(o => o.State)
            .HasColumnName("state")
            .IsRequired();

        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(oi => oi.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}