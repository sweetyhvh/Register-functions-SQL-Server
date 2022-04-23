using System;

namespace Register
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Users(int id, string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }

        public Users(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
