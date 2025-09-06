using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace User.Api.Core
{
    public class CreateUserBusiness
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public async Task<User> Process()
        {
            var response = await httpClient.GetStringAsync("https://randomuser.me/api/");
            using var doc = JsonDocument.Parse(response);
            var root = doc.RootElement.GetProperty("results")[0];
            var user = new User
            {
                FirstName = root.GetProperty("name").GetProperty("first").GetString(),
                LastName = root.GetProperty("name").GetProperty("last").GetString(),
                Email = root.GetProperty("email").GetString(),
                Username = root.GetProperty("login").GetProperty("username").GetString(),
                Password = root.GetProperty("login").GetProperty("password").GetString(),
                Birthday = DateTime.Parse(root.GetProperty("dob").GetProperty("date").GetString())
            };
            return user;
        }
    }
}