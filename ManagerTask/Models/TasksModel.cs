using ManagerTask.Models.Enuns;

namespace ManagerTask.Models;

public class TasksModel
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Data { get; set; } 

    public EnumTaskStatus Status { get; set; }
}
