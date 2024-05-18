using DTO;

namespace MVC.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetUser(int id);
        public Task<IEnumerable<UserDTO>> GetUsers();
        public Task<IEnumerable<UserDTO>> GetUsersWithoutAccount();
        public Task<UserDTO> CreateUser(UserDTO userDTO);
        public Task<UserDTO> UpdateUser(int id, UserDTO userDTO);
        public Task<UserDTO> DeleteUser(int id);
    }
}
