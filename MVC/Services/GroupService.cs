using DTO;
using MVC.Services.Interfaces;
using SQS = MVC.Services.QuerySnippet.StandardQuerySet;

namespace MVC.Services
{
    public class GroupService : IGroupService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public GroupService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseUrl = configuration["WebAPI:BaseUrl"] + "/groups";
        }

        public async Task<GroupDTO?> GetGroupById(int id)
        {
            return await SQS.Get<GroupDTO>(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<GroupDTO>?> GetAllGroups()
        {
            return await SQS.GetAll<GroupDTO>(_client, _baseUrl);
        }

        public async Task<GroupDTO?> CreateGroup(GroupDTO accountDTO)
        {
            return await SQS.Post<GroupDTO?>(_client, _baseUrl, accountDTO);
        }

        public async Task<Boolean> UpdateGroup(GroupDTO accountDTO)
        {
            return await SQS.PutNoReturn(_client, $"{_baseUrl}/{accountDTO.GroupId}", accountDTO);
        }

        public async Task<Boolean> DeleteGroup(int id)
        {
            return await SQS.Delete(_client, $"{_baseUrl}/{id}");
        }

        public async Task<IEnumerable<GroupDTO>?> GetGroupsByUserId(int userId)
        {
           return await SQS.Get<List<GroupDTO>>(_client, $"{_baseUrl}/User/{userId}");
        }
    }
}
