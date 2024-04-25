
using static System.Net.Mime.MediaTypeNames;

namespace Backend.Utils
{
    public class JustEncoding
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string ToMD5(string? plainText)
        {
  
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(plainText);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    return Convert.ToHexString(hashBytes).ToLower();
                }
            }

        }
    }
}
