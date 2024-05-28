﻿using DTO;
using MVC.Services.Interfaces;
using System.Text.Json;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public UserService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/users";
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            return await SQS.Get<UserDTO>(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsers()
        {
            return await SQS.GetAll<UserDTO>(_client, _baseUrl);
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsersActiveWithoutAccount()
        {
            return await SQS.GetAll<UserDTO>(_client, $"{_baseUrl}/ActiveNoAccount");
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsersActiveWithAccount()
        {
            return await SQS.GetAll<UserDTO>(_client, $"{_baseUrl}/ActiveWithAccount");
        }

        public async Task<UserDTO?> CreateUser(UserDTO userDTO)
        {
            return await SQS.Post<UserDTO?>(_client, _baseUrl, userDTO);
        }

        public async Task<Boolean> UpdateUser(UserDTO userDTO)
        {
            return await SQS.PutNoReturn(_client, $"{_baseUrl}/{userDTO.UserId}", userDTO);
        }

        public async Task<Boolean> DeleteUser(int id)
        {
            return await SQS.Delete(_client, $"{_baseUrl}/{id}");
        }
    }
}
