using Npgsql;
using tasks_list.src.Models;
using System.Data.Common;

namespace tasks_list.src.Mappers
{
    public class TaskMapper
    {
        public static TaskItem map(DbDataReader dataReader)
        {
            TaskItem task = new TaskItem();

            task.Id = dataReader.GetInt64(0);
            task.Title = dataReader.GetString(1);
            task.Description = dataReader.GetString(2);
            task.Owner = dataReader.GetString(3);
            task.IsDone = dataReader.GetBoolean(4);
            task.ListId = dataReader.GetInt64(5);
            
            return task;
        }
    }
}