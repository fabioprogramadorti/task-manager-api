using Microsoft.EntityFrameworkCore;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository? _repository;
    private readonly AppDbContext _context;

    public TaskService(ITaskRepository? repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        if (_repository != null)
            return await _repository.GetAllAsync();

        return await _context.TaskItems.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        if (_repository != null)
            return await _repository.GetByIdAsync(id);

        return await _context.TaskItems.FindAsync(id);
    }

    public async Task CreateAsync(TaskItem task)
    {
        task.Id = Guid.NewGuid();
        task.DataCriacao = DateTime.UtcNow;

        if (_repository != null)
        {
            await _repository.AddAsync(task);
            return;
        }

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        if (_repository != null)
        {
            await _repository.UpdateAsync(task);
            return;
        }

        _context.TaskItems.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        if (_repository != null)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task != null)
                await _repository.DeleteAsync(task);
            return;
        }

        var entity = await _context.TaskItems.FindAsync(id);
        if (entity != null)
        {
            _context.TaskItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TaskResponseDTO>> GetFilteredAsync(TaskFilterDTO filter)
    {
        var query = _context.TaskItems.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Status))
            query = query.Where(t => t.Status.ToString() == filter.Status);

        if (!string.IsNullOrEmpty(filter.Prioridade))
            query = query.Where(t => t.Prioridade.ToString() == filter.Prioridade);

        if (filter.UsuarioId.HasValue)
            query = query.Where(t => t.UsuarioId == filter.UsuarioId);

        return await query
            .OrderByDescending(t => t.DataCriacao)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TaskResponseDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataConclusao = t.DataConclusao,
                UsuarioId = t.UsuarioId
            })
            .ToListAsync();
    }

    public async Task<object> GetResumoAsync(Guid usuarioId)
    {
        return await _context.TaskItems
            .Where(t => t.UsuarioId == usuarioId)
            .GroupBy(t => t.Status)
            .Select(g => new
            {
                Status = g.Key.ToString(),
                Total = g.Count()
            })
            .ToListAsync();
    }
}