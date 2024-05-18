using DTO;
using MVC.Services.Interfaces;
using System.Text.Json;

namespace MVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7176/api/users";

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var response = await _client.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<List<UserDTO>>(responseBody, options);
            return users;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersWithoutAccount()
        {
            var response = await _client.GetAsync(_baseUrl + "/NoAccount");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<List<UserDTO>>(responseBody, options);
            return users;
        }

        public Task<UserDTO> UpdateUser(int id, UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
