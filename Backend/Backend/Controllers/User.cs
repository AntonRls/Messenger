using Backend.DB;
using Backend.Interface;
using Backend.JWTToken;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DBManager database;
        public UserController(DBManager database)
        {
            this.database = database;
        }
        [HttpGet("all")]
        public List<User> GetAllUsers()
        {
            return database.userManager.GetAllUser();
        }
        [HttpGet]
        public User GetUserInfo(int user_id = 0)
        {
            if (user_id == 0)
            {
                var token = Request.Headers.Authorization;

                var currentUser = new User(token, database.userManager);

                return currentUser;
            }
            else
            {
                var currentUser = new User(user_id, database.userManager);
                return currentUser;
            }
        }

        [HttpPost("image")]
        public ICustomResponse UploadImage([FromBody] ImageModel image)
        {
            var token = Request.Headers.Authorization;

            var currentUser = new User(token, database.userManager);

            if (currentUser == null || !currentUser.Token.ValidToken(token))
                return new BadRequest("Некорректный пользователь");

            byte[] bytes = Convert.FromBase64String(image.b64Image);
            System.IO.File.WriteAllBytes($"Images/UsersIcon/{currentUser.Id}.png", bytes);

            return new GoodResponse();
        }

        [HttpGet("image")]
        public async Task<IActionResult> GetImage(int user_id = 0)
        {
            Stream stream = null;
            if (user_id == 0)
            {
                var token = Request.Headers.Authorization;
                var currentUser = new User(token, database.userManager);

                if (currentUser == null || !currentUser.Token.ValidToken(token))
                    return null;

                if (System.IO.File.Exists($"Images/UsersIcon/{currentUser.Id}.png"))
                {
                    stream = System.IO.File.Open($"Images/UsersIcon/{currentUser.Id}.png", FileMode.Open);
                    if (stream == null)
                        return NotFound();
                    return File(stream, "application/octet-stream", "image.png"); 
                }
                stream = System.IO.File.Open($"Images/UsersIcon/base.png", FileMode.Open);
                if (stream == null)
                    return NotFound();
                return File(stream, "application/octet-stream", "image.png");
            }
            else
            {
                if (System.IO.File.Exists($"Images/UsersIcon/{user_id}.png"))
                {
                    stream = System.IO.File.Open($"Images/UsersIcon/{user_id}.png", FileMode.Open);
                    if (stream == null)
                        return NotFound();
                    return File(stream, "application/octet-stream", "image.png");
                }
                stream = System.IO.File.Open($"Images/UsersIcon/base.png", FileMode.Open);
                if (stream == null)
                    return NotFound();
                return File(stream, "application/octet-stream", "image.png");

            }
        }
    }
}
