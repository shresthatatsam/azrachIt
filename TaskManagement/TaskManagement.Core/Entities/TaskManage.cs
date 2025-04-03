namespace TaskManagement.TaskManagement.Core.Entities
{
    public class TaskManage
    {
        
        public Guid Id { get; set; }
        public string Title { get; set; }    
        public string? Description { get; set; } 
        public DateTime ExecutionDateTime { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
