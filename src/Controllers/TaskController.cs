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

        [HttpGet("lists")]
        public List<TaskList> GetAllLists()
        {
            List<TaskList> taskLists = taskListService.GetAll().ToList();
            
            foreach (var list in taskLists)
            {
                List<TaskItem> tasks = taskService.GetByListId(list.Id).ToList();
                list.Tasks = tasks;
            }

            return taskLists;
        }


        [HttpGet("lists/{listId}/tasks")]
        public TaskList GetAllListTasks(long listId)
        {
            TaskList taskList = taskListService.GetById(listId);
            List<TaskItem> tasks = taskService.GetByListId(listId).ToList();
            taskList.Tasks = tasks;
            return taskList;
        }

        [HttpPost("lists/{listId}/tasks")]
        public void CreateNewTask(TaskItem task, long listId)
        {
            taskService.CreateNew(task, listId);
        }

        [HttpPost("lists")]
        public void CreateNewTaskList(TaskList tasklist)
        {
            taskListService.CreateNew(tasklist);
        }

        [HttpPatch("lists/{listId}/tasks/{id}")]
        public void ChangeTaskStatus(long id, CompleteStatus status)
        {
            taskService.ChangeStatusById(id, status.IsDone);
        }

        [HttpDelete("tasks/{id}")]
        public void RemoveTaskById(long id)
        {
            taskService.RemoveById(id);
        }

        [HttpDelete("lists/{listId}")]
        public void RemoveTaskListById(long listId)
        {
            taskListService.RemoveById(listId);
        }
    }
}