namespace tasks_list.src.Models
{
    public class TaskDto
    {
        public long id {get; set;}
        public long list_id {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public string owner {get; set;}
        public bool isDone {get; set;}
    }
}