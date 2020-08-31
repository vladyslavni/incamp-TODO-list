using Npgsql;
using tasks_list.src.Models;

namespace tasks_list.src.Mappers
{
    public class TaskListMapper
    {
        public static TaskListDto map(NpgsqlDataReader dataReader)
        {
            TaskListDto taskList = new TaskListDto();

            taskList.id = dataReader.GetInt64(0);
            taskList.name = dataReader.GetString(1);
            
            return taskList;
        }
    }
}