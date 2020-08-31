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
        private TaskService taskService;
        private TaskListService taskListService;

        public TaskController(TaskService taskService, TaskListService taskListService)
        {
            this.taskService = taskService;
            this.taskListService = taskListService;
        }

        [HttpGet("tasks/{id}")]
        public TaskItem GetTaskById(int id)
        {
            return taskService.GetById(id);
        }
    
        [HttpGet("tasks")]
        public IEnumerable<TaskItem> GetAllTasks()
        {
            return taskService.GetAll();
        }

        [HttpGet("lists/{listId}/tasks")]
        public TaskList GetAllListTasks(int listId)
        {
            TaskList taskList = taskListService.GetById(listId);
            IEnumerable<TaskItem> tasks= taskService.GetByListId(listId);
            taskList.Tasks = tasks.ToList();
            return taskList;
        }

        [HttpPost("lists/{listId}/tasks")]
        public void CreateNewTask(TaskItem task, int listId)
        {
            taskService.CreateNew(task, listId);
        }

        [HttpPost("lists")]
        public void CreateNewTaskList(TaskList tasklist)
        {
            taskListService.CreateNew(tasklist);
        }

        [HttpPatch("lists/{listId}/tasks/{id}")]
        public void ChangeTaskStatus(int id, TaskStatus status)
        {
            taskService.ChangeStatusById(id, status.IsDone);
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