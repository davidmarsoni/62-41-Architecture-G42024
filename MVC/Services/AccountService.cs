
using DTO;
using MVC.Services.Interfaces;
using QS = MVC.Services.QuerySnippet.QuerySnippet;
using System.Net;
using System.Text.Json;

namespace MVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://localhost:7176/api/accounts";

        public AccountService(HttpClient client)
        {
            _client = client;
        }

        public async Task<AccountDTO> GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAccounts()
        {
            HttpResponseMessage? httpResponse = await QS.QueryOnURL(_client, _baseUrl);
            if (httpResponse == null)
            { return null; }
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<AccountDTO>>(responseBody, QS.JsonSerializerOpt);
            return accounts;
        }

        public async Task<AccountDTO> CreateAccount(AccountDTO accountDTO)
        {
            var response = await _client.PostAsJsonAsync(_baseUrl, accountDTO);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return JsonSerializer.Deserialize<AccountDTO>(
                                       responseBody, options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            } else
            {
                return null;
            }
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
