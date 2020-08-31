using Npgsql;
using tasks_list.src.Models;
using System.Data.Common;

namespace tasks_list.src.Mappers
{
    public class TaskListMapper
    {
        public static TaskList map(DbDataReader dataReader)
        {
            TaskList taskList = new TaskList();

            taskList.Id = dataReader.GetInt64(0);
            taskList.Name = dataReader.GetString(1);
            
            return taskList;
        }
    }
}