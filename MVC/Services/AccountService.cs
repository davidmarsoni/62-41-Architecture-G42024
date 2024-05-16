
using DTO;
using MVC.Services.Interfaces;
using System.Text.Json;

namespace MVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7176/api/account";

        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AccountDTO> GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AccountDTO>> GetAllAccounts()
        {
            var response = await _client.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var accounts = JsonSerializer.Deserialize<List<AccountDTO>>(responseBody, options);
            return accounts;
        }

        public async Task<AccountDTO> CreateAccount(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDTO> UpdateAccount(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
