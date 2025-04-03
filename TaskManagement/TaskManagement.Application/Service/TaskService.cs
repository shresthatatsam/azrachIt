using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.Tasks;
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
            try
            {
				//Added this for local Time 
				#region LocalTime
				dto.CreatedAt = DateTime.Now;
				dto.UpdatedAt = DateTime.Now;
				#endregion

				await _Context.Tasks.AddAsync(dto);
				await _Context.SaveChangesAsync();
				Log.Information("Task with id " + dto.Id + " Created Successfully");
				return dto;
			}
            catch(Exception ex)
            {
                Log.Information(ex.ToString());
                throw ex;
            }
          
        }

        public async Task<List<TaskManage>> GetList(TaskFilter dto)
        {

            if (dto == null)
            {
				Log.Information("Dto is null");
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

        public async Task<TaskManage> UpdateRecord(int id, TaskManageDto dto)
        {
            try
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
				Log.Information("data updated successfully");
				return data;
			}
			catch (Exception ex)
			{
				Log.Information(ex.ToString());
				throw ex;
			}

		}


        public async Task<bool> DeleteRecord(int id)
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


       public async Task<TaskManage> GetById(int id)
        {
            return await _Context.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

    }
}
