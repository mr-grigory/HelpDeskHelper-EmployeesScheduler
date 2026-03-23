using EmployeesScheduler.Infrastructure.Postgres;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<EmployeesSchedulerDbContext>(_ => 
    new EmployeesSchedulerDbContext(builder
        .Configuration
        .GetConnectionString("EmployeesSchedulerDb") ?? throw new NullReferenceException("connection string")));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();