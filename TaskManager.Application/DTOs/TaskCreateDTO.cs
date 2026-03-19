public class TaskCreateDTO
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public int Status { get; set; }
    public int Prioridade { get; set; }

    public Guid UsuarioId { get; set; }
}