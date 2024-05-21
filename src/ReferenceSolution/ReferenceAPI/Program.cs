using FluentValidation;
using Marten;
using Microsoft.FeatureManagement;
using ReferenceApi.Employees;
using ReferenceAPI.Employees;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("data") ?? throw new Exception("No connection");

builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);
}).UseLightweightSessions();

builder.Services.AddSingleton<INotifyOfPossibleSithLords, NotifyOfPossibleSithLords>();
builder.Services.AddScoped<ICheckForUniqueEmployeeStubs, EmployeeUniquenessChecker>();

builder.Services.AddScoped<IGenerateSlugsForNewEmployees, EmployeeSlugGeneratorWithUniqueIds>();
// Add services to the container.
builder.Services.AddFeatureManagement();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateRequestValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
