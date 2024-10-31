using Fitz_Roy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TareasController : ControllerBase
{
    private readonly TodoContext _context;

    public TareasController(TodoContext context)
    {
        _context = context;
    }

    // Endpoint GET: api/tarea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaItem>>> GetTareas()
    {
        return await _context.Tarea.ToListAsync();
    }

    // Endpoint GET: api/tarea/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TareaItem>> GetTarea(int id)
    {
        var tareaItem = await _context.Tarea.FindAsync(id);
        if (tareaItem == null)
        {
            return NotFound();
        }
        return tareaItem;
    }

    // Endpoint POST: api/tarea
    [HttpPost]
    public async Task<ActionResult<TareaItem>> CreateTarea(TareaItem tareaItem)
    {
        if (string.IsNullOrWhiteSpace(tareaItem.Title) || tareaItem.Title.Length > 100)
        {
            return BadRequest("El título es obligatorio y debe tener como máximo 100 caracteres.");
        }

        if (tareaItem.Description?.Length > 500)
        {
            return BadRequest("La descripción debe tener como máximo 500 caracteres.");
        }

        if (!IsValidStatus(tareaItem.Status))
        {
            return BadRequest("El estado de la tarea debe ser 'pendiente', 'en progreso' o 'completada'.");
        }

        tareaItem.CreatedAt = DateTime.UtcNow;
        _context.Tarea.Add(tareaItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTarea), new { id = tareaItem.Id }, tareaItem);
    }

    // Endpoint PUT: api/tarea/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTarea(int id, TareaItem tareaItem)
    {
        if (id != tareaItem.Id)
        {
            return BadRequest("El ID de la tarea no coincide.");
        }

        if (string.IsNullOrWhiteSpace(tareaItem.Title) || tareaItem.Title.Length > 100)
        {
            return BadRequest("El título es obligatorio y debe tener como máximo 100 caracteres.");
        }

        if (tareaItem.Description?.Length > 500)
        {
            return BadRequest("La descripción debe tener como máximo 500 caracteres.");
        }

        if (!IsValidStatus(tareaItem.Status))
        {
            return BadRequest("El estado de la tarea debe ser 'pendiente', 'en progreso' o 'completada'.");
        }

        _context.Entry(tareaItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TareaExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // Endpoint DELETE: api/tarea/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarea(int id)
    {
        var tareaItem = await _context.Tarea.FindAsync(id);
        if (tareaItem == null)
        {
            return NotFound();
        }

        _context.Tarea.Remove(tareaItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TareaExists(int id)
    {
        return _context.Tarea.Any(e => e.Id == id);
    }

    private bool IsValidStatus(string status)
    {
        return status == "pendiente" || status == "en progreso" || status == "completada";
    }
}
