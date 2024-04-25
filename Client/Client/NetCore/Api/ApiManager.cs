using Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client.NetCore.Api
{
    class ApiManager
    {
        public static async Task<string> RegisterUser(User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(user), null, "application/json");
                var result = await client.PostAsync($"{NetCoreConfig.ApiUrl}/register", content);
                string stringJson = await result.Content.ReadAsStringAsync();
  
                var json = JObject.Parse(stringJson);
                if (json.ContainsKey("response.error"))
                {
                    throw new Exception(json.SelectToken("response.error").ToString());
                }

                return json.SelectToken("response.answer").ToString();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task LoadImage(string token, string b64)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{NetCoreConfig.ApiUrl}/user/image");
                request.Headers.Add("Authorization", "JWT " + token);
                request.Content = new StringContent(JsonConvert.SerializeObject(new ImageModel
                {
                    b64Image = b64
                }), null, "application/json");
                var result = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<string> AuthUser(User user)
        {
            try
            {
                HttpClient client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(user), null, "application/json");
                var result = await client.PostAsync($"{NetCoreConfig.ApiUrl}/auth", content);
                string stringJson = await result.Content.ReadAsStringAsync();

                var json = JObject.Parse(stringJson);
                if (json.ContainsKey("response.error"))
                {
                    throw new Exception(json.SelectToken("response.error").ToString());
                }
                return json.SelectToken("response.answer").ToString();
            }
            catch
            {
                throw new Exception("Неверно указаны данные");
            }
        }
        public static async Task<User> GetUserInfo(string token, int user_id = 0)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{NetCoreConfig.ApiUrl}/user?user_id={user_id}");
                request.Headers.Add("Authorization", "JWT "+token);
                var result = await client.SendAsync(request);
                var stringJson = await result.Content.ReadAsStringAsync();
              
                var json = JObject.Parse(stringJson);
                if (stringJson.Length < 20)
                {
                    throw new Exception("Токен не валидный");
                }

                return JsonConvert.DeserializeObject<User>(stringJson);
            }
            catch(Exception ex) 
            {

                throw new Exception(ex.ToString());
            }
        }
        public static async Task<Dialog[]> GetDialogs(string token)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{NetCoreConfig.ApiUrl}/dialogs");
                request.Headers.Add("Authorization", "JWT " + token);
                var result = await client.SendAsync(request);
                var stringJson = await result.Content.ReadAsStringAsync();


                if (stringJson.Length < 20)
                {
                    throw new Exception("Токен не валидный");
                }

                return JsonConvert.DeserializeObject<Dialog[]>(stringJson);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
        public static async Task<Message[]> GetMessages(string token, int user_id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{NetCoreConfig.ApiUrl}/messages?user_id={user_id}");
                request.Headers.Add("Authorization", "JWT " + token);
                var result = await client.SendAsync(request);
                var stringJson = await result.Content.ReadAsStringAsync();

                if (stringJson.Length < 20)
                {
                    throw new Exception("Токен не валидный");
                }

                return JsonConvert.DeserializeObject<Message[]>(stringJson);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.ToString());
            }
        }
        public static async Task SendMessage(string token, MessageSendParams messageSend)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, $"{NetCoreConfig.ApiUrl}/messages");
                request.Headers.Add("Authorization", "JWT " + token);
                request.Content = new StringContent(JsonConvert.SerializeObject(messageSend), null, "application/json");
                var result = await client.SendAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static async Task<List<User>> GetAllUsers()
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{NetCoreConfig.ApiUrl}/user/all");
                var result = await client.SendAsync(request);
                var stringJson = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<User>>(stringJson);
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.ToString()); 
            }
        }
        public static string GetImage(int id)
        {
            try
            {
                return $"{NetCoreConfig.ApiUrl}/user/image?user_id={id}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
