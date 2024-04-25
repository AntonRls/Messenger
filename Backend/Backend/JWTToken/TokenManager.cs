using Backend.Models;
using Backend.Utils;
using Newtonsoft.Json;

namespace Backend.JWTToken
{
    public class TokenManager
    {
        User user;
        const string secretKey = "swJrefe3wde@11Suhnhah@8JA";

        public TokenManager(User user)
        {
            this.user = user;
        }

        public string GenerateToken()
        { 
            TokenHead head = new() { alg = "HS256", type= "JWT" };
            TokenBody body = new() { sub = user.Id, sta = DateTimeOffset.Now.ToUnixTimeSeconds() };

            string data = $"{JustEncoding.Base64Encode(JsonConvert.SerializeObject(head))}.{JustEncoding.Base64Encode(JsonConvert.SerializeObject(body))}";
            string cert = JustEncoding.ToMD5($"{secretKey}{data}");
     
            return $"{data}.{cert}";
        }
       
        public bool ValidToken(string token)
        {
            string[] nodes = token.Replace("JWT ", "").Trim().Split('.');
            string certTokenCert = JustEncoding.ToMD5($"{secretKey}{nodes[0]}.{nodes[1]}");
            return certTokenCert == nodes[2];
        }
        public TokenBody TokenInfo(string token)
        {
            string[] nodes = token.Split('.');
            return JsonConvert.DeserializeObject<TokenBody>(JustEncoding.Base64Decode(nodes[1]));
        }
    }
}
