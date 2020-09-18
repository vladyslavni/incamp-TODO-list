using System;
using System.Globalization;
using tasks_list.src.Models;
using tasks_list.src.Mappers;
using System.Collections.Generic;
using tasks_list.utils;
using Npgsql;
using NpgsqlTypes;
using System.Data.Common;

namespace tasks_list.Services
{
    public class TaskListService
    {
        public static NpgsqlConnection conn;

        public TaskListService()
        {
            conn = PGConnection.Get();
        }

        public TaskList GetById(long id)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, name from tasks_list where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                dr.Read();
                return TaskListMapper.map(dr);
            }
        }

        public IEnumerable<TaskList> GetAll()
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, name from tasks_list", conn);
            
            using (DbDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskListMapper.map(dr);
            }
        }
        public void CreateNew(TaskList tasklist)
        {
            using(NpgsqlCommand command = new NpgsqlCommand(
                "insert into tasks_list(name) values (@name);", conn))
            {
                command.Parameters.AddWithValue("name", NpgsqlDbType.Text, tasklist.Name);
                command.ExecuteNonQuery();  
            }
        }

        public void RemoveById(long id)
        {
            using(NpgsqlCommand command = new NpgsqlCommand("delete from tasks_list where id = @id", conn))
            {
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);

            command.ExecuteNonQuery();  
            }
        }
    }
}