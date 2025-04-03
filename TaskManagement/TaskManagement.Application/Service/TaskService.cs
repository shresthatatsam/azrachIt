using TaskManagement.TaskManagement.Application.Interface;
using TaskManagement.TaskManagement.Core.Entities;
using TaskManagement.TaskManagement.Infrastructure.Data;

namespace TaskManagement.TaskManagement.Application.Service
{
    public class TaskService : ITaskService
    {
        public readonly ApplicationDbContext _Context;
        public TaskService(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public async Task<TaskManage> Create(TaskManage dto)
        {
            //Added this for local Time 
            #region LocalTime
            dto.ExecutionDateTime = DateTime.Now;
            dto.CreatedAt = DateTime.Now; 
            dto.UpdatedAt = DateTime.Now;
            #endregion

            await _Context.Tasks.AddAsync(dto);
            await _Context.SaveChangesAsync();
            return dto;
        }


    }
}
