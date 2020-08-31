using System.Collections.Generic;

namespace tasks_list.src.Models
{
    public class TaskListDto
    {
        public long id {get; set;}
        public string name {get; set;}
        public List<TaskDto> tasks {get; set;}
    }
}