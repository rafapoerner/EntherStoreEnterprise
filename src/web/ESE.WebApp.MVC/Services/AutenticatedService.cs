using ESE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace ESE.WebApp.MVC.Services
{
    public class AutenticatedService : IAutenticatedService
    {
        private readonly HttpClient _httpClient;

        public AutenticatedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLogin),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44336/api/identity/autenticated", loginContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(userRegister),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44336/api/identity/new-account", registerContent);

            return JsonSerializer.Deserialize<UserResponseLogin>(await response.Content.ReadAsStringAsync());
        }
    }
}
