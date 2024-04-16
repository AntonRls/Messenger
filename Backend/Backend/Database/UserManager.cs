using Backend.DB;
using Backend.Models;
using System.Data.SQLite;

namespace Backend.Database
{
    public class UserManager
    {
        DBManager manager;
        public UserManager(DBManager manager)
        {
            this.manager = manager;
        }

        public void InitDB(SQLiteConnection connection)
        {
            var command = new SQLiteCommand(connection);

            command.CommandText = @"
                 CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Password TEXT NOT NULL
                );";

            command.ExecuteNonQuery();
            command.Clone();
        }

        public void InsertUser(User user)
        {
            var connection = manager.GetConnection();
            var command = new SQLiteCommand(connection);
            command.CommandText = @"INSERT INTO Users (Name, LastName, Password) VALUES (@name, @lastName, @pass)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@lastName", user.LastName);
            command.Parameters.AddWithValue("@pass", user.EncodePassword());
            command.ExecuteNonQuery();
        }
    }
}
