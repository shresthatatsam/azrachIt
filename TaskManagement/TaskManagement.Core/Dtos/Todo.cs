using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Core.Dtos
{
    public class Todo
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
