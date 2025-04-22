using Microsoft.EntityFrameworkCore;
using Serilog;
using Tiles.Core.ServiceContracts;
using Tiles.Core.Services;

using Tiles.Infrastructure.Repositories;

using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;
using Tiles.Core.Domain.RepositroyContracts;

using Tiles.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS to allow requests from your React app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React app URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog as the logging provider

// Database context (PostgreSQL connection string)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection for Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Dependency Injection for User Management
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline
// Swagger configuration
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger.json", "Tiles API");
    // c.RoutePrefix = string.Empty; // Uncomment to host at root
});


// Enable CORS before controllers
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

