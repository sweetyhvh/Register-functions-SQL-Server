using System;
using System.Data.SqlClient;

namespace Register
{
    public abstract class DB
    {
        private string _connectionString;
        protected SqlConnection _connection;

        public DB(string server, string db, bool trusted_connection)
        {
            _connectionString = $"Data Source={server}; Initial Catalog={db}; Trusted_Connection={trusted_connection}"; //Using windows auth
        }

        //Using sql login
        /*public DB(string server, string db, string user, string password)
        {
            _connectionString = $"Data Source={server}; Initial Catalog={db}; User={user}; Password={password}";
        }*/

        //Connect to DB function
        public void Connect()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        //Close connection from DB Function
        public void Close()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
