using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;
using Xunit;

namespace TaskManager.Tests;

public class TaskServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateTask_Should_Save_Task()
    {
        
        var context = GetDbContext();

        var service = new TaskService(null!, context);

        var task = new TaskItem
        {
            Titulo = "Teste",
            Descricao = "Descrição teste",
            UsuarioId = Guid.NewGuid()
        };

    
        await service.CreateAsync(task);

        
        var result = await context.TaskItems.FirstOrDefaultAsync();

        result.Should().NotBeNull();
        result!.Titulo.Should().Be("Teste");
    }

    [Fact]
    public async Task GetAll_Should_Return_List()
    {
        
        var context = GetDbContext();

        context.TaskItems.Add(new TaskItem
        {
            Id = Guid.NewGuid(),
            Titulo = "Task 1",
            Descricao = "Desc",
            UsuarioId = Guid.NewGuid()
        });

        await context.SaveChangesAsync();

        var service = new TaskService(null!, context);

        
        var result = await service.GetAllAsync();

        
        result.Should().NotBeEmpty();
    }
}