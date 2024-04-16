namespace Backend
{
    public class Responses
    {
        public static Dictionary<string, string> GoodResponse()
        {
            return new Dictionary<string, string> { { "response", "ok" } };
        }
        public static Dictionary<string, string> BadRequest()
        {
            return new Dictionary<string, string> { { "error", "bad request" } };
        }
    }
}
