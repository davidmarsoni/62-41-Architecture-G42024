using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class UserMapper
    {
        public static UserDTO toDTO (User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.UserId = user.Id;
            userDTO.Username = user.Username;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;
            userDTO.Email = user.Email;
            return userDTO;
        }

        public static User toDAL(UserDTO userDTO)
        {
            User user = new User
            {
                Id = userDTO.UserId,
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email
            };
            return user;
        }
    }
}
