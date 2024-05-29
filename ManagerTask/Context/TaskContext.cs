using Microsoft.EntityFrameworkCore;
using ManagerTask.Models;

namespace ManagerTask.Context;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public DbSet<TasksModel> Tasks { get; set; }
}
