using Npgsql;
using tasks_list.src.Models;

namespace tasks_list.src.Mappers
{
    public class TaskMapper
    {
        public static TaskDto map(NpgsqlDataReader dataReader)
        {
            TaskDto task = new TaskDto();

            task.id = dataReader.GetInt64(0);
            task.title = dataReader.GetString(1);
            task.description = dataReader.GetString(2);
            task.owner = dataReader.GetString(3);
            task.isDone = dataReader.GetBoolean(4);
            task.list_id = dataReader.GetInt64(5);
            
            return task;
        }
    }
}