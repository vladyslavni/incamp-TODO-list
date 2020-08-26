using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tasks_list.Models;
using tasks_list.Services;

namespace tasks_list.Controllers
{
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        TaskService service;
        public TaskController(TaskService service)
        {
            this.service = service;
        }

        [HttpGet("/{id}")]
        public TaskDto GetTaskById(int id)
        {
            return service.GetById(id);
        }
    
        [HttpGet]
        public List<TaskDto> GetAllTasks()
        {
            return service.GetAll();
        }

        [HttpPost]
        public void CreateNewTask(TaskDto task)
        {
            service.CreateNew(task);
        }

        [HttpPut("{id}")]
        public void ChangeTaskStatus(int id, [FromQuery] bool status)
        {
            service.ChangeStatusById(id, status);
        }

        [HttpDelete("{id}")]
        public void RemoveTaskById(int id)
        {
            service.RemoveById(id);
        }
    }
}
