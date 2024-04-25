using Backend.Database;
using Backend.JWTToken;
using Backend.Utils;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        private string? _password { get; set; }
        public string? Password
        {
            get {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public TokenManager? Token;
        public string? Image { get; set; }
        public User()
        {
            Token = new TokenManager(this);
        }
        public User(string token, UserManager userManager)
        {
            token = token.Replace("JWT", "");
            Token = new TokenManager(this);
            var id = Token.TokenInfo(token).sub;
            Console.WriteLine(token);
            var user = userManager.GetUser(id);
            Id = user.Id; Name = user.Name; LastName = user.LastName;
           
        }
        public User(int id, UserManager userManager)
        {
            var user = userManager.GetUser(id);
            Id = user.Id; Name = user.Name; LastName = user.LastName; 
      
        }


        private string EncodePassword(string? password)
        {
            return JustEncoding.ToMD5(password);
        }
    }
}
