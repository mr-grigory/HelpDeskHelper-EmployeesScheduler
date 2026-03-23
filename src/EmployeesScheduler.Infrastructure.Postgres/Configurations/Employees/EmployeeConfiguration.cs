using EmployeesScheduler.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeesScheduler.Infrastructure.Postgres.Configurations.Employees;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(e => e.Id).HasName("pk_employees");

        builder.Property(e => e.Id).HasColumnName("id");
        
        builder.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
        
    }
}
