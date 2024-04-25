using Backend.DB;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dialogs : ControllerBase
    {
        DBManager database;
        public Dialogs(DBManager dBManager) {
            database = dBManager;
        }
        [HttpGet]
        public object GetDialogs(int offset = 0)
        {
            var token = Request.Headers.Authorization;
            var currentUser = new User(token, database.userManager);

            if (currentUser == null || !currentUser.Token.ValidToken(token))
                return new BadRequest("Некорректный пользователь");
            return database.dialogsManager.GetDialogs(currentUser.Id, offset);
        }
 
    }
}
