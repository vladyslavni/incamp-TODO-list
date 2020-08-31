using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tasks_list.src.Models;
using tasks_list.Services;
using System.Linq;
using System;

namespace tasks_list.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TaskController : Controller
    {
        private static TaskService taskService;
        private static TaskListService taskListService;

        static TaskController()
        {
            taskService = new TaskService();
            taskListService = new TaskListService();
        }

        [HttpGet("tasks/{id}")]
        public TaskDto GetTaskById(int id)
        {
            return taskService.GetById(id);
        }
    
        [HttpGet("tasks")]
        public IEnumerable<TaskDto> GetAllTasks()
        {
            return taskService.GetAll();
        }

        [HttpGet("lists/{listId}/tasks")]
        public TaskListDto GetAllListTasks(int listId)
        {
            TaskListDto taskList = taskListService.GetById(listId);
            IEnumerable<TaskDto> tasks= taskService.GetByListId(listId);
            taskList.tasks = tasks.ToList();
            return taskList;
        }

        [HttpPost("lists/{listId}/tasks")]
        public void CreateNewTask(TaskDto task, int listId)
        {
            taskService.CreateNew(task, listId);
        }

        [HttpPost("lists")]
        public void CreateNewTaskList(TaskListDto tasklist)
        {
            taskListService.CreateNew(tasklist);
        }

        [HttpPatch("lists/{listId}/tasks/{id}")]
        public void ChangeTaskStatus(int id, TaskStatus status)
        {
            taskService.ChangeStatusById(id, status.isDone);
        }

        [HttpDelete("tasks/{id}")]
        public void RemoveTaskById(int id)
        {
            taskService.RemoveById(id);
        }

        [HttpDelete("lists/{listId}")]
        public void RemoveTaskListById(int listId)
        {
            taskListService.RemoveById(listId);
        }
    }
}