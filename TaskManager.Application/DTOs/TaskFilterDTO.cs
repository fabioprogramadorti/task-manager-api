namespace TaskManager.Application.DTOs;

public class TaskFilterDTO
{
    public string? Status { get; set; }
    public string? Prioridade { get; set; }
    public Guid? UsuarioId { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}