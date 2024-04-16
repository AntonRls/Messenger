using Backend.DB;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        DBManager database;
        public Register(DBManager database)
        {
            this.database = database;
        }
        [HttpPost]
        public Dictionary<string, string> CreateUser([FromBody] User user)
        {
            if (user.Name.Length < 2 || user.LastName.Length < 2 || user.Password.Length < 3)
                return Responses.BadRequest();
            database.userManager.InsertUser(user);
            return Responses.GoodResponse();
        }
    }
}
