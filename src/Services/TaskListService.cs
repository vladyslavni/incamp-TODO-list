using tasks_list.src.Models;
using tasks_list.src.Mappers;
using System.Collections.Generic;
using tasks_list.utils;
using Npgsql;
using NpgsqlTypes;

namespace tasks_list.Services
{
    public class TaskListService
    {
        public static NpgsqlConnection conn;

        public TaskListService()
        {
            conn = PGConnection.Get();
        }

        public TaskListDto GetById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, name from tasks_list where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                dr.Read();
                return TaskListMapper.map(dr);
            }
        }

        public IEnumerable<TaskListDto> GetAll()
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, name from tasks_list", conn);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskListMapper.map(dr);
            }
        }

        public void CreateNew(TaskListDto tasklist)
        {
            using(NpgsqlCommand command = new NpgsqlCommand(
                "insert into tasks_list(name) values (@name);", conn))
            {
                command.Parameters.AddWithValue("name", NpgsqlDbType.Text, tasklist.name);

                command.ExecuteNonQuery();  
            }
        }

        public void RemoveById(int id)
        {

            using(NpgsqlCommand command = new NpgsqlCommand("delete from tasks_list where id = @id", conn))
            {
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);

            command.ExecuteNonQuery();  
            }
        }
    }
}