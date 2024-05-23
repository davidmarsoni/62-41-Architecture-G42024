using DTO;

namespace MVC.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<GroupDTO?> GetGroupById(int id);
        public Task<IEnumerable<GroupDTO>?> GetAllGroups();
        public Task<GroupDTO?> CreateGroup(GroupDTO accountDTO);
        public Task<Boolean> UpdateGroup(GroupDTO accountDTO);
        public Task<Boolean> DeleteGroup(int id);
    }
}
