using TaskManagement.TaskManagement.Core.Dtos;
using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Application.Interface
{
    public interface ITaskService
    {
        Task<TaskManage> Create(TaskManage dto);
        Task<List<TaskManage>> GetList(TaskFilter dto);

        Task<TaskManage> GetById(int id);
        Task<TaskManage> UpdateRecord(int id, TaskManageDto dto);
        Task<bool> DeleteRecord(int id);
    }
}
