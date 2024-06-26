﻿
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
        private readonly string _baseUrl;

        public AccountService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/accounts";
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
            return await SQS.Post<AccountDTO?>(_client, _baseUrl, accountDTO);
        }

        public async Task<Boolean> UpdateAccount(AccountDTO accountDTO)
        {
            return await SQS.PutNoReturn(_client, $"{_baseUrl}/{accountDTO.AccountId}", accountDTO);
        }

        public async Task<Boolean> DeleteAccount(int id)
        {
            return await SQS.Delete(_client, $"{_baseUrl}/{id}");
        }

        public async Task<AccountDTO?> GetAccountByUserId(int userId)
        {
            return await SQS.Get<AccountDTO>(_client, $"{_baseUrl}/User/{userId}");
        }
    }
}
