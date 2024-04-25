using Backend.Context;
using Backend.DB;
using Backend.Models;
using System.Data.SQLite;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Database
{
    public class MessagesManager
    {
        DBManager manager;
        public MessagesManager(DBManager manager)
        {
            this.manager = manager;
        }

        public List<Message> GetMessages(int dialogId, int offset)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = new List<Message>();
                var messages = db.Messages.Where(message => message.DialogId == dialogId).ToList();

                foreach(var message in messages)
                {
                    result.Add(new Message
                    {
                        DialogId = message.DialogId,
                        From = new User(message.FromUser, manager.userManager),
                        To = new User(message.ToUser, manager.userManager),
                        Text = message.Text,
                        TimeCreate = message.TimeCreate,
                        MessageId = message.MessageId
                    });
                }
                return result;
            }
        }
        public void InsertMessage(int dialogId, string text, int from, int to)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Messages.Add(new Models.Message
                {
                    DialogId = dialogId,
                    Text = text,
                    FromUser = from,
                    ToUser = to,
                    TimeCreate = DateTimeOffset.Now.ToUnixTimeSeconds()
                });
                db.SaveChanges();
            }
        }
    }
}
