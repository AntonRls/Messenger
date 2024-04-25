using Backend.DB;
using Backend.Interface;
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
        public ICustomResponse CreateUser([FromBody] User user)
        {
            if (user.Name.Length < 2 || user.LastName.Length < 2 || user.Password.Length < 3)
                return new BadRequest();
            database.userManager.InsertUser(user);
            return new GoodResponse(user.Token.GenerateToken());
        }
    }
}
