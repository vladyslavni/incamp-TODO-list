namespace tasks_list.Models
{
    public class TaskDto
    {

        public long id {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public string owner {get; set;}
        public bool isDone {get; set;}


        public TaskDto()
        {}

        public TaskDto(long id, string title, string description, string owner, bool isDone)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.owner = owner;
            this.isDone = isDone;
        }
    }
}