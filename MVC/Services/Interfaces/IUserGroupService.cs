using DTO;

namespace MVC.Services.Interfaces
{
    public interface IUserGroupService
    {

        public Task<UserGroupDTO?> GetUserGroupById(int id);
        public Task<IEnumerable<UserGroupDTO>?> GetAllUserGroups();
        public Task<UserGroupDTO?> CreateUserGroup(UserGroupDTO userGroupDTO);
        public Task<Boolean> DeleteUserGroup(int id);
    }
}
