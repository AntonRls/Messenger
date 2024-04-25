
using Backend.Interface;

namespace Backend
{
    public class GoodResponse : ICustomResponse
    {
        public Dictionary<string, string> Response { get; set; }
        public GoodResponse()
        {
            this.Response = new Dictionary<string, string> { { "answer", "ok" } };
        }
        public GoodResponse(string text)
        {
            this.Response = new Dictionary<string, string> { { "answer", text } };
        }
        public GoodResponse(string name, string text)
        {
            this.Response = new Dictionary<string, string> { { name, text } };
        }
    }
    public class BadRequest : ICustomResponse
    {
        public Dictionary<string, string> Response { get; set; }

        public BadRequest()
        {
            this.Response = new Dictionary<string, string> { { "error", "bad request" } };
        }
        public BadRequest(string text)
        {
            this.Response = new Dictionary<string, string> { { "error", text } };
        }
    }

}
