using tasks_list.Models;
using System.Collections.Generic;
using System;
using Npgsql;
using NpgsqlTypes;

namespace tasks_list.Services
{
    public class TaskService
    {
        public static NpgsqlConnection conn;

        public TaskService()
        {
            conn = Program.CreateConnection();
            conn.Open();
        }

        public TaskDto GetById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, description, " + 
            "owner, is_done from tasks where id = @id", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                dr.Read();
                return TaskMapper.map(dr);
            }
        }
        public IEnumerable<TaskDto> GetAll()
        {
            NpgsqlCommand command = new NpgsqlCommand("select id, title, description, " + 
            "owner, is_done from tasks", conn);
            
            using (NpgsqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                    yield return TaskMapper.map(dr);
            }
        }

        public void CreateNew(TaskDto task)
        {
            using(NpgsqlCommand command = new NpgsqlCommand(
                "insert into tasks(title, description, owner) values (@title, @description, @owner);", conn))
            {
                command.Parameters.AddWithValue("title", NpgsqlDbType.Text, task.title);
                command.Parameters.AddWithValue("description", NpgsqlDbType.Text, task.description);
                command.Parameters.AddWithValue("owner", NpgsqlDbType.Text, task.owner);

                command.Prepare();

                command.ExecuteNonQuery();  
            }
        }
        public void ChangeStatusById(int id, bool status)
        {
            NpgsqlCommand command = new NpgsqlCommand("update tasks set is_done = @is_done where id = @id;", conn);
            
            command.Parameters.AddWithValue("is_done", NpgsqlDbType.Boolean, status);
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);

            command.ExecuteNonQuery();  
        }

        public void RemoveById(int id)
        {
            NpgsqlCommand command = new NpgsqlCommand("delete from tasks where id = @id;", conn);
            
            command.Parameters.AddWithValue("id", NpgsqlDbType.Integer, id);

            command.ExecuteNonQuery();  
        }
    }
}