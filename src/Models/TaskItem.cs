namespace tasks_list.src.Models
{
    public class TaskItem
    {
        public long Id {get; set;}
        public long ListId {get; set;}
        public string Title {get; set;}
        public bool IsDone {get; set;}
    }
}