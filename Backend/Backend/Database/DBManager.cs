using Backend.Database;
using System.Data.SQLite;

namespace Backend.DB
{
    public class DBManager
    {
        public const string PathDB = "database.db";
        public const string ConnectionString = $"Data Source={PathDB};Version=3;";

        public UserManager userManager;

        public DBManager()
        {
            if (!File.Exists(PathDB))
                SQLiteConnection.CreateFile(PathDB);

            userManager = new UserManager(this);

            var connection = GetConnection();

            userManager.InitDB(connection);

            connection.Close();
        }

        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
