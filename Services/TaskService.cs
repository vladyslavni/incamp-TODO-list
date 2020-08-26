using tasks_list.Models;
using System.Collections.Generic;
using System.Linq;

namespace tasks_list.Services
{
    public class TaskService
    {
        private List<TaskDto> tasks = new List<TaskDto>();

        public TaskDto GetById(int id)
        {
            return tasks.Where(task => task.id == id).First();
        }
    
        public List<TaskDto> GetAll()
        {
            return tasks;
        }

        public void CreateNew(TaskDto task)
        {
            tasks.Add(task);
        }

        public void ChangeStatusById(int id, bool status)
        {
            TaskDto task = GetById(id);
            task.isDone = status;
        }

        public void RemoveById(int id)
        {
            TaskDto task = GetById(id);
            tasks.Remove(task);
        }
    }
}