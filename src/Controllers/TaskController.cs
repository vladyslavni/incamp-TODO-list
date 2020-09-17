using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using tasks_list.src.Models;
using tasks_list.Services;
using System.Linq;
using System;
using System.Threading.Tasks;


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
        public async IAsyncEnumerable<TaskList> GetAllLists()
        {
            IEnumerable<TaskList> lists = taskListService.GetAll().ToEnumerable();
            foreach (var list in lists)
            {
                list.Tasks = await taskService.GetByListId(list.Id).ToListAsync();
                yield return list;
            }
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