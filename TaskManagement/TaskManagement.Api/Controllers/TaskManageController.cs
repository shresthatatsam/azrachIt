using Microsoft.AspNetCore.Mvc;
using TaskManagement.TaskManagement.Application.Interface;
using TaskManagement.TaskManagement.Core.Dtos;
using TaskManagement.TaskManagement.Core.Entities;

namespace TaskManagement.TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManageController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskManageController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<TaskManage> Create(TaskManage dto)
        {
            return await _taskService.Create(dto);

        }

        [HttpPost("getByIdAndList")]
        public async Task<List<TaskManage>> Get(TaskFilter dto)
        {
            var data = _taskService.GetList(dto);
            return await data;
        }

        [HttpPut("{id}")]
        public async Task<TaskManage> UpdateRecord(Guid id, [FromBody] TaskManageDto dto)
        {
            var updatedTask = await _taskService.UpdateRecord(id, dto);
            return updatedTask;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteRecord(Guid id)
        {
            await _taskService.DeleteRecord(id);
            return true;
        }

    }
}
