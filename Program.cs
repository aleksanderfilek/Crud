using System;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace crud
{
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = OpenConnection("127.0.0.1", "root", "root", "crud");
            if(connection == null)
            {
                return;
            }

            AddUser(connection, "Marek", "Kido", 37);
            UpdateUserAge(connection, "Aleksander", "Filek", 18);
            Console.WriteLine("");
            GetUsers(connection);
            Console.WriteLine("");
            RemoveUser(connection, "Aleksander", "Filek");
            Console.WriteLine("");
            GetUsers(connection);
            connection.Close();
        }

        static MySqlConnection OpenConnection(string serverIP, string login, string password, string database)
        {
            if(String.IsNullOrEmpty(serverIP))
                return null;

            if(String.IsNullOrEmpty(login))
                return null;

            if(String.IsNullOrEmpty(password))
                return null;

            if(String.IsNullOrEmpty(database))
                return null;

            try
            {
                string myConnectionString = "server=" + serverIP + ";uid=" + login + ";" +
                                "pwd=" + password + ";database=" + database;

                MySqlConnection connection = new MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
                Console.WriteLine("[Connection] - Opened");

                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static bool AddUser(MySqlConnection connection, string firstName, string lastName, byte age)
        {
            if(String.IsNullOrEmpty(firstName))
                return false;
            
            if(String.IsNullOrEmpty(lastName))
                return false;

            string sql = "INSERT INTO users (FirstName, LastName, Age) VALUES ('" + firstName + "', '" + lastName + "', " + age +")";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            Console.WriteLine("[Connection] - User {0} {1} added", "Marek", "Kido");

            return true;
        }

        static bool RemoveUser(MySqlConnection connection, string firstName, string lastName)
        {
            if(String.IsNullOrEmpty(firstName))
                return false;
            
            if(String.IsNullOrEmpty(lastName))
                return false;

            string sql = "DELETE FROM users WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            Console.WriteLine("[Connection] - User {0} {1} removed", firstName, lastName);

            return true;
        }

        static void GetUsers(MySqlConnection connection)
        {
            string sql = "SELECT FirstName, LastName, Age FROM users";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine("[Connection] - Got user info");
            Console.WriteLine("Users table");
            while (rdr.Read())
            {
                Console.WriteLine("{0}, {1}, age: {2}", rdr[0], rdr[1], rdr[2]);
            }
            rdr.Close();
        }

        static bool UpdateUserAge(MySqlConnection connection, string firstName, string lastName, byte age)
        {
            if(String.IsNullOrEmpty(firstName))
                return false;
            
            if(String.IsNullOrEmpty(lastName))
                return false;

            string sql = "UPDATE users SET Age = " + age + " WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            Console.WriteLine("[Connection] - User {0} {1} updated", firstName, lastName);

            return true;
        }
    }
}
