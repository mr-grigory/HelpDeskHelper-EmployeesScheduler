using Microsoft.EntityFrameworkCore;

namespace EmployeesScheduler.Infrastructure.Postgres;

public class EmployeesSchedulerDbContext(string connectionString) : DbContext
{
    private readonly string _connectionString = connectionString;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeesSchedulerDbContext).Assembly);
    }
}