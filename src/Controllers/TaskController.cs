using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tasks_list.src.Models;
using tasks_list.Services;
using System;

namespace tasks_list.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TaskController : Controller
    {
        private static TaskService service;

        static TaskController()
        {
            service = new TaskService();
        }

        [HttpGet("tasks/{id}")]
        public TaskDto GetTaskById(int id)
        {
            return service.GetById(id);
        }
    
        [HttpGet("tasks")]
        public IEnumerable<TaskDto> GetAllTasks()
        {
            return service.GetAll();
        }

        [HttpGet("lists/{listId}/tasks")]
        public IEnumerable<TaskDto> GetAllListTasks(int listId)
        {
            return service.GetByListId(listId);
        }

        [HttpPost("lists/{listId}/tasks")]
        public void CreateNewTask(TaskDto task, int listId)
        {
            service.CreateNew(task, listId);
        }

        [HttpPost("lists")]
        public void CreateNewTaskList(TaskListDto tasklist)
        {
            service.CreateNewList(tasklist);
        }

        [HttpPatch("lists/{listId}/tasks/{id}")]
        public void ChangeTaskStatus(int id, TaskStatus status)
        {
            service.ChangeStatusById(id, status.isDone);
        }

        [HttpDelete("lists/{listId}/tasks/{id}")]
        public void RemoveTaskById(int id)
        {
            service.RemoveById(id);
        }
        
        [HttpDelete("lists/{listId}")]
        public void RemoveTaskListById(int listId)
        {
            service.RemoveListById(listId);
        }
    }
}