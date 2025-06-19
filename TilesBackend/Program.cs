using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Tiles.Core.ServiceContracts;
using Tiles.Core.Services;
using Tiles.Infrastructure.Data;
using Tiles.Infrastructure.Repositories;
using TilesBackendApI.Middleware;
using Tiles.Core.ServiceContracts.UserManagement.Application.Interfaces;
using Tiles.Core.Domain.RepositroyContracts;
using Tiles.Core.Domain.RepositoryContracts;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configure Serilog Logging
// -------------------------
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog(); // Integrate Serilog

// -------------------------
// CORS Policy for React Frontend
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
// PostgreSQL DbContext Setup
// -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// -------------------------
// JWT Authentication Setup
// -------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// -------------------------
// Add Controllers, JSON Settings, and Swagger
// -------------------------
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Match Node.js _id and camelCase behavior
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; // Allow custom validation
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------------
// MemoryCache for Rate Limiting
// -------------------------
builder.Services.AddMemoryCache();

// -------------------------
// Dependency Injection (Repositories & Services)
// -------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ISellerService, SellerService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();

builder.Services.AddScoped<IEcatalogCategoryRepository,EcatalogCategoryRepository>();
builder.Services.AddScoped<IEcatalogCategoryService, EcatalogCategoryService>();

builder.Services.AddScoped<IEcatalogRepository, EcatalogRepository>();
builder.Services.AddScoped<IEcatalogService, EcatalogService>();

builder.Services.AddScoped<IInstagramRepository, InstagramRepository>();
builder.Services.AddScoped<IInstagramService, InstagramService>();


// -------------------------
// Build and Configure the App
// -------------------------
var app = builder.Build();

// Enable Swagger (even in prod if needed)
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

// -------------------------
// Rate Limiting Middleware
// -------------------------

// General API limit: 500 requests / 10 minutes
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api") &&
                       !context.Request.Path.StartsWithSegments("/api/auth") &&
                       !context.Request.Path.StartsWithSegments("/api/upload"), appBuilder =>
                       {
                           appBuilder.UseCustomRateLimit(
                               maxRequests: 500,
                               window: TimeSpan.FromMinutes(10),
                               limitMessage: "Too many requests from this IP, please try again later."
                           );
                       });

// Auth limit: 15 login attempts / hour
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/auth"), appBuilder =>
{
    appBuilder.UseCustomRateLimit(
        maxRequests: 15,
        window: TimeSpan.FromHours(1),
        limitMessage: "Too many login attempts, please try again later."
    );
});

// Upload limit: 100 uploads / hour
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/upload"), appBuilder =>
{
    appBuilder.UseCustomRateLimit(
        maxRequests: 100,
        window: TimeSpan.FromHours(1),
        limitMessage: "Too many upload attempts, please try again later."
    );
});

// -------------------------
// Authentication, Authorization, and Controller Mapping
// -------------------------
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
