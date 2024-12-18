using Microsoft.AspNetCore.Mvc;
using KanbanBoardAPI.Models;
using KanbanBoardAPI.Interfaces;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : ControllerBase where TEntity : class, IHaveId, new()
    {
        private readonly KanbanContext _context;

        public BaseController(KanbanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var entities = await _context.Set<TEntity>().ToListAsync();
            Console.WriteLine(JsonSerializer.Serialize(entities));

            return entities;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest("ID сущности не совпадает с переданным ID.");
            }

            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();  // Возвращаем 204 No Content, если обновление прошло успешно
        }

        // Удалить сущность по ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();  // Возвращаем 204 No Content, если удаление прошло успешно
        }

        // Проверить, существует ли сущность с данным ID
        private bool EntityExists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }

        [HttpPut("{taskId}/move")]
        public async Task<IActionResult> MoveTaskToColumn(int taskId, [FromBody] int newColumnId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            task.ColumnId = newColumnId;  // Перемещаем задачу в другую колонку
            await _context.SaveChangesAsync();

            return NoContent();  // Возвращаем 204 No Content
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredTasks([FromQuery] string assignee, [FromQuery] bool? isCompleted)
        {
            var tasksQuery = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(assignee))
            {
                tasksQuery = tasksQuery.Where(t => t.Assignee == assignee);
            }

            if (isCompleted.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.IsCompleted == isCompleted.Value);
            }

            var tasks = await tasksQuery.ToListAsync();

            return Ok(tasks);
        }

    }
}