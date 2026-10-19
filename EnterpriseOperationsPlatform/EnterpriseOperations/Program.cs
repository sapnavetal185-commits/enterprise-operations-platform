using EnterpriseOperations.Authentication;
using EnterpriseOperations.Data;
using EnterpriseOperations.Exceptions;
using EnterpriseOperations.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<TravelHubDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TravelHubDb")));

builder.Services.AddDbContext<TravelHubIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TravelHubAuthDb")));

builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TravelHubIdentityDbContext>()
    .AddSignInManager();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("api", limiter =>
    {
        limiter.PermitLimit = 100;
        limiter.Window = TimeSpan.FromMinutes(1);
    });
});

var app = builder.Build();
app.UseMiddleware<RequestLoggingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRateLimiter();

app.MapControllers();

app.Run();
