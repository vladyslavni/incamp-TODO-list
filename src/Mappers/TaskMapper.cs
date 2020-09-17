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
            task.IsDone = dataReader.GetBoolean(2);
            task.ListId = dataReader.GetInt64(3);
            
            return task;
        }
    }
}