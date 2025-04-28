using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using FluentValidation;
using AutoMapper;
using ExpenseStatistics.DB;
using ExpenseStatistics.Auth;
using ExpenseStatistics.Services;
using ExpenseStatistics.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Логування (Serilog)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// Підключення БД (EF Core + PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Реєстрація сервісів
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AuthService>();

// Валідація
builder.Services.AddValidatorsFromAssemblyContaining<CreateTransactionDtoValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

// Мапінг (AutoMapper)
builder.Services.AddAutoMapper(typeof(Program));

// Кешування
builder.Services.AddMemoryCache();

// Аутентифікація та Авторизація (JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] 
                ?? throw new InvalidOperationException("Jwt:Key is missing")))
        };
    });

// Контролери
builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Swagger (API документація)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ExpenseStatistics API",
        Version = "v1"
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseStatistics API v1");
        c.RoutePrefix = string.Empty; // Swagger UI за адресою http://localhost:5000/
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExpenseStatistics API v1");
        c.RoutePrefix = string.Empty; // Swagger UI за адресою http://localhost:5000/
    });
}

// Логування HTTP-запитів
app.UseSerilogRequestLogging();

// Перевірка токену користувача
app.UseAuthentication();
// Перевірка прав доступу
app.UseAuthorization();
// Підключення контролерів
app.MapControllers();

app.Run();
