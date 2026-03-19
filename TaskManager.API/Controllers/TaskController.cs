using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tasks = await _service.GetAllAsync();
        return Ok(tasks);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _service.GetByIdAsync(id);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDTO dto)
    {
        var task = new TaskItem
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Status = (StatusTarefa)dto.Status,
            Prioridade = (Prioridade)dto.Prioridade,
            UsuarioId = dto.UsuarioId
        };

        await _service.CreateAsync(task);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TaskCreateDTO dto)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        existing.Titulo = dto.Titulo;
        existing.Descricao = dto.Descricao;
        existing.Status = (StatusTarefa)dto.Status;
        existing.Prioridade = (Prioridade)dto.Prioridade;
        existing.UsuarioId = dto.UsuarioId;

        await _service.UpdateAsync(existing);

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var existing = await _service.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        await _service.DeleteAsync(id);

        return NoContent();
    }

    
    [HttpGet("filter")]
    public async Task<IActionResult> GetFiltered([FromQuery] TaskFilterDTO filter)
    {
        var result = await _service.GetFilteredAsync(filter);
        return Ok(result);
    }

    
    [HttpGet("resumo/{usuarioId}")]
    public async Task<IActionResult> GetResumo(Guid usuarioId)
    {
        var resumo = await _service.GetResumoAsync(usuarioId);
        return Ok(resumo);
    }
}