using Backend.Database;
using Backend.DB;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Messages : ControllerBase
    {
        DBManager database;
        public Messages(DBManager database)
        {
            this.database = database;
        }

        [HttpPost]
        public ICustomResponse SendMessage([FromBody] MessageSendModel message)
        {

            var token = Request.Headers.Authorization;
            var currentUser = new User(token, database.userManager);

            if (currentUser == null || !currentUser.Token.ValidToken(token))
                return new BadRequest("Некорректный пользователь");

            try
            {
                var dialogId = database.dialogsManager.GetDialogId(currentUser.Id, message.UserId);
                database.messagesManager.InsertMessage(dialogId, message.Text, currentUser.Id, message.UserId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return new GoodResponse();
        }
        [HttpGet]
        public object GetMessages(int user_id, int offset = 0)
        {
            var token = Request.Headers.Authorization;
            var currentUser = new User(token, database.userManager);

            if (currentUser == null || !currentUser.Token.ValidToken(token))
                return new BadRequest("Некорректный пользователь");
            var dialogId = database.dialogsManager.GetDialogId(currentUser.Id, user_id);
            return database.messagesManager.GetMessages(dialogId, user_id);
        }
    }
}
