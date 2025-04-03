using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Core.Dtos
{
    public class TaskManageDto
    {
    
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public StatusEnum Status { get; set; }
        
    }
}
