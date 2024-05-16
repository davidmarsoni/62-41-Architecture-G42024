using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class GroupMapper
    {
        public static GroupDTO toDTO (Group group)
        {
            GroupDTO groupDTO = new GroupDTO();
            groupDTO.GroupId = group.Id;
            groupDTO.Name = group.Name;
            groupDTO.Acronym = group.Acronym;
            return groupDTO;
        }

        public static Group toDAL(GroupDTO groupDTO)
        {
            Group group = new Group 
            {
                Id = groupDTO.GroupId,
                Name = groupDTO.Name,
                Acronym = groupDTO.Acronym
            };
            return group;
        }

        // map list of users
        
        public static GroupDTO addUsersCollection(Group group, GroupDTO groupDTO)
        {
            groupDTO.UserDTOs = new List<UserDTO>();
            foreach (User user in group.Users)
            {
                groupDTO.UserDTOs.Add(UserMapper.toDTO(user));
            }
            return groupDTO;
        }

        public static Group addUsersCollection(GroupDTO groupDTO, Group group)
        {
            // no need to create new list of users -> ICollection create itself
            foreach (UserDTO userDTO in groupDTO.UserDTOs)
            {
                group.Users.Add(UserMapper.toDAL(userDTO));
            }
            return group;
        }
    }
}
