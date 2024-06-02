using DTO;
using MVC.Services.Interfaces;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public UserGroupService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/UserGroup";
        }
        public async Task<UserGroupDTO?> CreateUserGroup(UserGroupDTO userGroupDTO)
        {
            return await SQS.Post<UserGroupDTO>(_client, _baseUrl, userGroupDTO);
        }

        public async Task<bool> DeleteUserGroup(int id)
        {
            return await SQS.Delete(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<UserGroupDTO>?> GetAllUserGroups()
        {
            return await SQS.GetAll<UserGroupDTO>(_client, _baseUrl);
        }

        public async Task<UserGroupDTO?> GetUserGroupById(int id)
        {
            return await SQS.Get<UserGroupDTO>(_client, $"{_baseUrl}/{id}");
        }
    }
}
