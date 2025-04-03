using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Application.Interface
{
    public interface ITaskService
    {
        Task<TaskManage> Create(TaskManage dto);

    }
}
