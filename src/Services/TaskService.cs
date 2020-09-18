using tasks_list.src.Models;
using tasks_list.src.Mappers;
using System.Collections.Generic;
using System;
using Npgsql;
using NpgsqlTypes;
using tasks_list.utils;

namespace tasks_list.Services
{
    public class TaskService
    {
        public static NpgsqlConnection conn;

        public TaskService()
        {
            conn = PGConnection.Get();
        }

        public IEnumerable<TaskItem> GetByListId(long listId)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, " +
                "is_done, list_id from tasks where list_id = @list_id", conn);
            
            command.Parameters.AddWithValue("list_id", NpgsqlDbType.Bigint, listId);

            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskMapper.map(dr);
            }
        }

        public IEnumerable<TaskItem> GetAll()
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, " + 
            "is_done, list_id from tasks", conn);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskMapper.map(dr);
            }
        }

        public void CreateNew(TaskItem task, long listId)
        {
            using(NpgsqlCommand command = new NpgsqlCommand(
                "insert into tasks(title, list_id) values (@title, @list_id);", conn))
            {
                command.Parameters.AddWithValue("title", NpgsqlDbType.Text, task.Title);
                command.Parameters.AddWithValue("list_id", NpgsqlDbType.Bigint, listId);

                command.Prepare();

                command.ExecuteNonQuery();  
            }
        }

        public void ChangeStatusById(long id, bool status)
        {
            NpgsqlCommand command = new NpgsqlCommand("update tasks set is_done = @is_done where id = @id", conn);
            
            command.Parameters.AddWithValue("is_done", NpgsqlDbType.Boolean, status);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);

            command.ExecuteNonQuery();  
        }

        public void RemoveById(long id)
        {
            NpgsqlCommand command = new NpgsqlCommand("delete from tasks where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Bigint, id);

            command.ExecuteNonQuery();  
        }
    }
}