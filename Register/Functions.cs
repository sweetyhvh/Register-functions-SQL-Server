using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Register
{
    public class Functions : DB
    {
        public Functions(string server, string db, bool trusted_connection) : base(server, db, trusted_connection)
        {
            //Empty
        }

        //Get all users
        public List<Users> GetUsers()
        {
            Connect();
            var users = new List<Users>();
            string query = "SELECT Id, Username, Password FROM Users";
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string username = reader.GetString(1);
                string password = reader.GetString(2);
                users.Add(new Users(id, username, password));
            }
            Close();
            return users;
        }

        //Get all but without id
        public Users Get(int id)
        {
            Connect();
            Users users = null;
            string query = "SELECT Id, Username, Password FROM Users WHERE id = @id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string username = reader.GetString(1);
                string password = reader.GetString(2);
                users = new Users(id, username, password);
            }
            Close();
            return users;
        }

        //Insert Function
        public void Add(Users users)
        {
            Connect();
            string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@username", users.Username);
            command.Parameters.AddWithValue("@password", users.Password);
            command.ExecuteNonQuery();

            Close();
        }

        //Edit Function
        public void Edit(Users users)
        {
            Connect();
            string query = "UPDATE Users SET Username=@username, Password=@password WHERE id=@id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@username", users.Username);
            command.Parameters.AddWithValue("@password", users.Password);
            command.Parameters.AddWithValue("@id", users.Id);
            command.ExecuteNonQuery();

            Close();
        }

        //Delete Function
        public void Delete(int id)
        {
            Connect();
            string query = "DELETE FROM Users WHERE id=@id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            Close();
        }
    }
}
