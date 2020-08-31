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

        public TaskDto GetById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, description, " + 
            "owner, is_done, list_id from tasks where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                dr.Read();
                return TaskMapper.map(dr);
            }
        }
        
        public IEnumerable<TaskDto> GetByListId(int listId)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, description, " + 
            "owner, is_done, list_id from tasks where list_id = @list_id", conn);
            
            command.Parameters.AddWithValue("list_id", NpgsqlDbType.Integer, listId);

            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskMapper.map(dr);
            }
        }

        public IEnumerable<TaskDto> GetAll()
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, description, " + 
            "owner, is_done, list_id from tasks", conn);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskMapper.map(dr);
            }
        }

        public void CreateNew(TaskDto task, int listId)
        {
            Console.WriteLine(listId);
            using(NpgsqlCommand command = new NpgsqlCommand(
                "insert into tasks(title, description, owner, list_id) values (@title, @description, @owner, @list_id);", conn))
            {
                command.Parameters.AddWithValue("title", NpgsqlDbType.Text, task.title);
                command.Parameters.AddWithValue("description", NpgsqlDbType.Text, task.description);
                command.Parameters.AddWithValue("owner", NpgsqlDbType.Text, task.owner);
                command.Parameters.AddWithValue("list_id", NpgsqlDbType.Integer, listId);

                command.Prepare();

                command.ExecuteNonQuery();  
            }
        }

        public void ChangeStatusById(int id, bool status)
        {
            NpgsqlCommand command = new NpgsqlCommand("update tasks set is_done = @is_done where id = @id", conn);
            
            command.Parameters.AddWithValue("is_done", NpgsqlDbType.Boolean, status);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);

            command.ExecuteNonQuery();  
        }

        public void RemoveById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("delete from tasks where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);

            command.ExecuteNonQuery();  
        }
    }
}