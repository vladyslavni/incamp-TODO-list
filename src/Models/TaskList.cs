using System.Collections.Generic;

namespace tasks_list.src.Models
{
    public class TaskList
    {
        public long Id {get; set;}
        public string Name {get; set;}
        public List<TaskItem> Tasks {get; set;}
    }
}