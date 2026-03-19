using TaskManager.Domain.Entities;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
namespace TaskManager.Application.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(Guid id);

    
    Task<List<TaskResponseDTO>> GetFilteredAsync(TaskFilterDTO filter);
    
    Task<object> GetResumoAsync(Guid usuarioId);
}