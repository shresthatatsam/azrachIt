using Microsoft.AspNetCore.Mvc;
using TaskManagement.TaskManagement.Application.Interface;
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
    }
}
