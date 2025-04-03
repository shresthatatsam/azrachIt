using Microsoft.EntityFrameworkCore;
using TaskManagement.TaskManagement.Application.Interface;
using TaskManagement.TaskManagement.Core.Dtos;
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

        public async Task<List<TaskManage>> GetList(TaskFilter dto)
        {

            if (dto == null)
            {
                throw new Exception("Dto is null");
            }

            var taskManage = _Context.Tasks.Where(x => x.IsActive == true).AsQueryable();

            if (dto.Id != null)
            {
                taskManage = _Context.Tasks.Where(x => x.Id == dto.Id).AsQueryable();
            }

            if (dto.Title != null)
            {
                taskManage = _Context.Tasks.Where(x => x.Title == dto.Title).AsQueryable();
            }

            return await taskManage.ToListAsync();
        }

        public async Task<TaskManage> UpdateRecord(Guid id, TaskManageDto dto)
        {
            var data = _Context.Tasks.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {

                data.Description = dto.Description;
                data.IsActive = dto.IsActive;
                data.Title = dto.Title;
                data.Status = dto.Status;
            }
            await _Context.SaveChangesAsync();
            return data;
        }


        public async Task<bool> DeleteRecord(Guid id)
        {
            var taskManage = await _Context.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (taskManage == null)
            {
                return false;
            }
            _Context.Tasks.Remove(taskManage);
            await _Context.SaveChangesAsync();
            return true;
        }

    }
}
