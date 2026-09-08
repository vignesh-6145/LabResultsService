using LabResultsService.API.Infrastructure;
using LabResultsService.Repository;
using LabResultsService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add services to the container.

builder.Services.AddControllers();

// Swagger Config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog Config
builder.Services.AddSerilog((services, lc) => lc
       .ReadFrom.Configuration(builder.Configuration)
       .ReadFrom.Services(services));

builder.Services.RegisterServices();
builder.Services.RegisterContext(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("connectionString"));
builder.Services.RegisterRepositories();

var app = builder.Build();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
