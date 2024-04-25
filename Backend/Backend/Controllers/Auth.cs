using Backend.DB;
using Backend.Interface;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        DBManager database;
        public Auth (DBManager database)
        {
            this.database = database;
        }

        [HttpPost]
        public ICustomResponse AuthUser([FromBody] User user)
        {
            if (!database.userManager.AuthUser(user))
                return new BadRequest("Неверный логин или пароль");

            return new GoodResponse(user.Token.GenerateToken());
        }

    }
}
