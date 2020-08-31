namespace tasks_list.src.Models
{
    public class TaskListDto
    {
        public long id {get; set;}
        public string name {get; set;}
 
        public TaskListDto()
        {}

        public TaskListDto(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}