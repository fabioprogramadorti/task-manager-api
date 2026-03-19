using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs;

public class TaskResponseDTO
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public StatusTarefa Status { get; set; }
    public Prioridade Prioridade { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; set; }
    public Guid UsuarioId { get; set; }
}