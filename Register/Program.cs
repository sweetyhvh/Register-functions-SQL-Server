using System;
using System.Data.SqlClient;


namespace Register
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool trusted_connection;
                Functions functions = new Functions("Server name", "Data Base Name", trusted_connection = true); //Using windows auth
                //Functions functions = new Functions("Server name", "Data Base Name", "Username", "Password"); //Using sql login
                bool again = true;
                int op = 0;

                do
                {
                    ShowMenu();
                    Console.WriteLine("\nSelect an option: ");
                    op = int.Parse(Console.ReadLine());

                    switch (op)
                    {
                        case 1:
                            Show(functions); //Calling Show function
                            break;
                        case 2:
                            Add(functions); //Calling Add function
                            break;
                        case 3:
                            Edit(functions); //Calling Edit function
                            break;
                        case 4:
                            Delete(functions); //Calling Delete function
                            break;
                        case 5:
                            again = false; //Don't show the menu again :D
                            break;
                    }

                } while (again);

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Can't connect to the DataBase");

                //To get the exact error uncomment that line
                //Console.WriteLine(ex);
            }
        }

        //Menu
        public static void ShowMenu()
        {
            Console.WriteLine("\n----------> Menu <----------");
            Console.WriteLine("1 -> Show all users");
            Console.WriteLine("2 -> Add User");
            Console.WriteLine("3 -> Edit User");
            Console.WriteLine("4 -> Delete User");
            Console.WriteLine("5 -> Exit");
        }

        //Show Users
        public static void Show(Functions functions)
        {
            Console.Clear();
            Console.WriteLine("Users from the DataBase");
            var users = functions.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Password: {user.Password}");
            }
        }

        //Add Users
        public static void Add(Functions functions)
        {
            Console.Clear();
            Console.WriteLine("Add a new user to the data base");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            Users users = new Users(username, password);
            functions.Add(users);
        }

        //Edit Users
        public static void Edit(Functions functions)
        {
            Console.Clear();
            Show(functions);
            Console.WriteLine("Edit a user");
            Console.Write("Write the id of the user to edit: ");
            int id = int.Parse(Console.ReadLine());

            Users users = functions.Get(id);
            if (users != null)
            {
                Console.Write("Type the username: ");
                string username = Console.ReadLine();
                Console.Write("Type user password: ");
                string password = Console.ReadLine();

                users.Username = username;
                users.Password = password;
                functions.Edit(users);
            }
            else
            {
                Console.WriteLine("That users doesn't exist");
            }
        }

        //Delete Users
        public static void Delete(Functions functions)
        {
            Console.Clear();
            Show(functions);
            Console.WriteLine("Delete a user");
            Console.Write("Type the user id of the user to delete: ");
            int id = int.Parse(Console.ReadLine());

            Users users = functions.Get(id);
            if (users != null)
            {
                functions.Delete(id);
            }
            else
            {
                Console.WriteLine("The user doesn't exist");
            }
        }
    }
}
