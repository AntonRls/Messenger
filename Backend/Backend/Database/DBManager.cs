using Backend.Database;
using System.Data.SQLite;

namespace Backend.DB
{
    public class DBManager
    {
        public UserManager userManager;
        public DialogsManager dialogsManager;
        public MessagesManager messagesManager;

        public DBManager()
        {
            userManager = new UserManager(this);
            dialogsManager = new DialogsManager(this);
            messagesManager = new MessagesManager(this);
        }
    }
}
