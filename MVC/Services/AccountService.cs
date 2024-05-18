
using DTO;
using MVC.Services.Interfaces;
using QS = MVC.Services.QuerySnippet.QuerySnippet;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;
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

        public async Task<AccountDTO?> GetAccountById(int id)
        {
            return await SQS.Get<AccountDTO>(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<AccountDTO>?> GetAllAccounts()
        {
            return await SQS.GetAll<AccountDTO>(_client, $"{_baseUrl}");
        }

        public async Task<AccountDTO?> CreateAccount(AccountDTO accountDTO)
        {
            HttpResponseMessage? httpResponse = await QS.PostOnUrl(_client, _baseUrl, accountDTO);
            if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.Created)
            {
                String responseBody = await httpResponse.Content.ReadAsStringAsync();
                return QS.JsonDeserialize<AccountDTO>(responseBody);
            } else
            {
                return null;
            }
        }

        public async Task<AccountDTO?> UpdateAccount(AccountDTO accountDTO)
        {
            HttpResponseMessage? httpResponse = await QS.PutOnUrl(_client, $"{_baseUrl}/{accountDTO.AccountId}", accountDTO);
            if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                String responseBody = await httpResponse.Content.ReadAsStringAsync();
                return QS.JsonDeserialize<AccountDTO>(responseBody);
            } else
            {
                return null;
            }
        }

        public async Task<Boolean> DeleteAccount(int id)
        {
            HttpResponseMessage? httpResponse = await QS.DeleteOnUrl(_client, $"{_baseUrl}/{id}");
            if (httpResponse != null && httpResponse.StatusCode != HttpStatusCode.NoContent)
            {
                Console.WriteLine($"DeleteAccount failed with status code: { httpResponse.StatusCode}");
                return false;
            }
            return true;
        }
    }
}
