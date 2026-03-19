using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    // =========================
    // 🔹 CREATE
    // =========================
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDTO dto)
    {
        var user = await _service.CreateAsync(dto.Nome, dto.Email, dto.Senha);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new
        {
            user.Id,
            user.Nome,
            user.Email
        });
    }

    // =========================
    // 🔹 GET ALL
    // =========================
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _service.GetAllAsync();

        return Ok(users.Select(u => new
        {
            u.Id,
            u.Nome,
            u.Email
        }));
    }

    // =========================
    // 🔹 GET BY ID
    // =========================
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Nome,
            user.Email
        });
    }

    // =========================
    // 🔹 UPDATE
    // =========================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserCreateDTO dto)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        user.Nome = dto.Nome;
        user.Email = dto.Email;

        await _service.UpdateAsync(user);

        return NoContent();
    }

    // =========================
    // 🔹 DELETE
    // =========================
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var user = await _service.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        await _service.DeleteAsync(id);

        return NoContent();
    }
}