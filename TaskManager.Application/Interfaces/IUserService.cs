using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface IUserService
{
    Task<Usuario> CreateAsync(string nome, string email, string senha);

    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(Guid id);
}