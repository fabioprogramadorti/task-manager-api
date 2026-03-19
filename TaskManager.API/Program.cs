using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;

using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.Validators;

using TaskManager.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// =========================
// 🔗 BANCO
// =========================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// =========================
// 📦 DI
// =========================
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IUserService, UserService>(); // 🔥 USER

// =========================
// ✅ FLUENT VALIDATION
// =========================
builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateValidator>();

// =========================
// 📄 SWAGGER
// =========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =========================
// 🚀 APP
// =========================
var app = builder.Build();

// =========================
// 🧱 MIDDLEWARE GLOBAL DE ERRO
// =========================
app.UseMiddleware<ExceptionMiddleware>();

// =========================
// 🌐 PIPELINE
// =========================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();