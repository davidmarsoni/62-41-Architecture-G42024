using DTO;

namespace MVC.Services.Interfaces
{
    public interface IUser_GroupService
    {
        public Task<GroupDTO?> GetUser_GroupById(int id);
        public Task<IEnumerable<GroupDTO>?> GetAllUser_Groups();
        public Task<GroupDTO?> CreateUser_Group(GroupDTO accountDTO);
        public Task<Boolean> DeleteUser_Group(int id);
    }
}
