using DTO;

namespace MVC.Services.Interfaces
{
    public interface IUserService
    {
        public UserDTO GetUser(int id);
        public List<UserDTO> GetUsers();
        public UserDTO CreateUser(UserDTO userDTO);
        public UserDTO UpdateUser(int id, UserDTO userDTO);
        public UserDTO DeleteUser(int id);
    }
}
