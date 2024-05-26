using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class UserMapper
    {
        public static UserDTO toDTO (User user)
        {
            UserDTO userDTO = new UserDTO
            {
                UserId = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Address = user.Address,
                IsDeleted = user.IsDeleted
            };
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
                Email = userDTO.Email,
                Gender = userDTO.Gender,
                Address = userDTO.Address,
                IsDeleted = userDTO.IsDeleted
            };
            return user;
        }
    }
}
