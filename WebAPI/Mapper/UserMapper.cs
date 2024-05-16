using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class UserMapper
    {
        public static UserDTO toDTO (User user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;
            userDTO.Email = user.Email;
            return userDTO;
        }

        public static User toDAL(UserDTO userDTO)
        {
            User user = new User
            {
                Id = userDTO.Id,
                Username = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email
            };
            return user;
        }

        // map list of groups
        public static UserDTO addGroupsCollection(User user, UserDTO userDTO)
        {
            userDTO.GroupDTOs = new List<GroupDTO>();
            foreach (Group group in user.Groups)
            {
                userDTO.GroupDTOs.Add(GroupMapper.toDTO(group));
            }
            return userDTO;
        }

        public static User addGroupsCollection(UserDTO userDTO, User user)
        {
            // no need to create new list of users -> ICollection create itself
            foreach (GroupDTO groupDTO in userDTO.GroupDTOs)
            {
                user.Groups.Add(GroupMapper.toDAL(groupDTO));
            }
            return user;
        }
    }
}
