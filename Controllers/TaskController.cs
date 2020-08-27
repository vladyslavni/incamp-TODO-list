using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tasks_list.Models;
using tasks_list.Services;
using System;

namespace tasks_list.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : Controller
    {
        private static TaskService service;

        static TaskController()
        {
            service = new TaskService();
        }

        [HttpGet("{id}")]
        public TaskDto GetTaskById(int id)
        {
            return service.GetById(id);
        }
    
        [HttpGet]
        public IEnumerable<TaskDto> GetAllTasks()
        {
            return service.GetAll();
        }

        [HttpPost]
        public TaskDto CreateNewTask(TaskDto task)
        {
            Console.WriteLine(task);
            service.CreateNew(task);

            return task;
        }

        [HttpPut("{id}")]
        public void ChangeTaskStatus(int id, bool status)
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