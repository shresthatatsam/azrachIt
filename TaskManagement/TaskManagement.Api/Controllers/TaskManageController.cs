using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
        private readonly ILogger<TaskManageController> _logger;
        public TaskManageController(ITaskService taskService, ILogger<TaskManageController> logger)
        {
            _taskService = taskService;
            _logger = logger;   
        }

        [HttpPost]
        public async Task<TaskManage> Create(TaskManage dto)
        {
            var data = await _taskService.Create(dto);
            Log.Information("Task with ID {TaskId} has been successfully created.", data.Id);
            BackgroundJob.Schedule(() => ExecuteTask(data.Id), data.ExecutionDateTime);

            return data;
        }

        [HttpPost("getByIdAndList")]
        public async Task<List<TaskManage>> Get(TaskFilter dto)
        {
            var data = _taskService.GetList(dto);
            return await data;
        }

        [HttpGet("id")]
        public async Task<TaskManage> Get(int id)
        {
            var data = _taskService.GetById(id);
            return await data;
        }


        [HttpPut("{id}")]
        public async Task<TaskManage> UpdateRecord(int id, [FromBody] TaskManageDto dto)
        {
            var updatedTask = await _taskService.UpdateRecord(id, dto);
            return updatedTask;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteRecord(int id)
        {
            await _taskService.DeleteRecord(id);
            return true;
        }

        [HttpGet]
        public async Task<TaskManage> ExecuteTask(int taskId)
      {
            using var client = new HttpClient();

            var response = await client.GetStringAsync($"https://jsonplaceholder.typicode.com/todos/{taskId}");

            var todo = JsonConvert.DeserializeObject<Todo>(response);
            if(todo.Completed == true)
            {
                var data = new TaskManageDto
                {
                    IsActive = true,
                    Title = todo.Title,
                    Status = StatusEnum.Completed
                };

               await _taskService.UpdateRecord(taskId, data);
            }
              var task =  await _taskService.GetById(taskId);
            return task;
        }


    }
}
