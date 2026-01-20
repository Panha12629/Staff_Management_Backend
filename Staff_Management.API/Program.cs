using MediatR;
using Microsoft.EntityFrameworkCore;
using Staff_Management.Application.Common.Interfaces;
using Staff_Management.Application.Features.Staffs;
using Staff_Management.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.AllowAnyOrigin()   // For dev only, allow React frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register MediatR
builder.Services.AddMediatR(typeof(StaffCreateCommandHandler).Assembly);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AppDbContext interface
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

var app = builder.Build();

// Middleware order is critical
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **Use CORS before HTTPS redirection and controllers**
app.UseCors("AllowReact");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
