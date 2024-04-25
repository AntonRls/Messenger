using Backend.Context;
using Backend.DB;
using Backend.Models;
using System.Data.SQLite;

namespace Backend.Database
{
    public class DialogsManager
    {
        DBManager manager;
        public DialogsManager(DBManager manager)
        {
            this.manager = manager;
        }
        public int CreateDialog(int id1, int id2)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Dialogs.Add(new Models.Dialog
                {
                    Id1 = id1,
                    Id2 = id2
                });
                db.SaveChanges();
                return db.Dialogs.OrderByDescending(dialog => dialog.DialogId).First().DialogId;
            }
        }
        public int GetDialogId(int id1, int id2)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var dialogs = db.Dialogs.Where(dialog => (dialog.Id1 == id1 && dialog.Id2 == id2) || (dialog.Id2 == id1 && dialog.Id1 == id2)); 
                if(dialogs.Count() > 0)
                {
                    return dialogs.First().DialogId;
                }
                return CreateDialog(id1, id2);
            }
        }
        public List<Dialog> GetDialogs(int id, int offset)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var result = new List<Dialog>();
                var dialogs = db.Dialogs.Where(dialog => dialog.Id1 == id || dialog.Id2 == id);

                foreach (var dialog in dialogs)
                {
                    try
                    {
                        User userWith = null;

                        if (dialog.Id1 == id)
                        {
                            userWith = new User(dialog.Id2, manager.userManager);
                        }
                        else
                        {
                            userWith = new User(dialog.Id1, manager.userManager);
                        }


                        result.Add(new Dialog
                        {
                            DialogId = dialog.DialogId,
                            LastMessage = manager.messagesManager.GetMessages(dialog.DialogId, 0).OrderByDescending(message => message.TimeCreate).First(),
                            User = userWith
                        });
                    }
                    catch { }         
                }
                return result.OrderByDescending(dialog => dialog.LastMessage.TimeCreate).ToList();

            }
        }
    }
}
