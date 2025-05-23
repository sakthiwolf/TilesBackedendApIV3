using Microsoft.EntityFrameworkCore;
using Serilog;
using Tiles.Core.ServiceContracts;
using Tiles.Core.Services;
using Tiles.Infrastructure.Data;
using Tiles.Infrastructure.Repositories;
using TilesBackendApI.Middleware;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.Domain.RepositoryContracts;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configure Serilog logging
// -------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Integrate Serilog

// -------------------------
// CORS policy for React frontend
// -------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// -------------------------
// PostgreSQL DbContext setup
// -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// -------------------------
// Add controllers and Swagger
// -------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------------
// MemoryCache for rate limiting
// -------------------------
builder.Services.AddMemoryCache();

// -------------------------
// Dependency Injection: Product & User Services
// -------------------------
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// -------------------------
// Dependency Injection: Category & Subcategory
// -------------------------
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// -------------------------
// Dependency Injection: Seller Repository and Seller Service
// -------------------------


builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ISellerService, SellerService>();


// -------------------------
// Build and configure app
// -------------------------
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

// -------------------------
// Custom Middleware: Rate Limiting
// -------------------------
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api") &&
                       !context.Request.Path.StartsWithSegments("/api/auth") &&
                       !context.Request.Path.StartsWithSegments("/api/upload"), appBuilder =>
                       {
                           appBuilder.UseCustomRateLimit(100, TimeSpan.FromMinutes(15), "Too many requests from this IP, please try again later.");
                       });

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/auth"), appBuilder =>
{
    appBuilder.UseCustomRateLimit(5, TimeSpan.FromHours(1), "Too many login attempts, please try again later.");
});

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/upload"), appBuilder =>
{
    appBuilder.UseCustomRateLimit(10, TimeSpan.FromHours(1), "Too many upload attempts, please try again later.");
});

// -------------------------
// Authorization and Controllers
// -------------------------
app.UseAuthorization();
app.MapControllers();
app.Run();
