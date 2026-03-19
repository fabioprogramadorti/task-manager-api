using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public StatusTarefa Status { get; set; }
    public Prioridade Prioridade { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; }

    // Relacionamento
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}