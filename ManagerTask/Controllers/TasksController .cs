using ManagerTask.Context;
using ManagerTask.Models;
using ManagerTask.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerTask.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController  : ControllerBase
{
    private readonly TaskContext _context;

    public TasksController(TaskContext context)
    {
        _context = context;
    }
    private readonly ILogger<TasksController > _logger;

    // GET: api/Tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }
    // GET: api/Tasks/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TasksModel>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }
    // PUT: api/Tasks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, TasksModel task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    // POST: api/Tasks
    [HttpPost]
    public async Task<ActionResult<Task>> PostTask(TasksModel task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTask", new { id = task.Id }, task);
    }

    // DELETE: api/Tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasksByName(string name)
    {
        var tasks = await _context.Tasks
                                  .Where(t => t.Titulo.Contains(name))
                                  .ToListAsync();

        if (tasks == null || tasks.Count == 0)
        {
            return NotFound();
        }

        return tasks;
    }
    [HttpGet("date/{date}")]
    public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasksByDate(DateTime date)
    {
        var tasks = await _context.Tasks
                                  .Where(t => t.Data.Date == date.Date)
                                  .ToListAsync();

        if (tasks == null || tasks.Count == 0)
        {
            return NotFound();
        }

        return tasks;
    }

    // GET: api/Tasks/status/{status}
    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasksByStatus(EnumTaskStatus status)
    {
        var tasks = await _context.Tasks
                                  .Where(t => t.Status == status)
                                  .ToListAsync();

        if (tasks == null || tasks.Count == 0)
        {
            return NotFound();
        }

        return tasks;
    }
    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }

}
