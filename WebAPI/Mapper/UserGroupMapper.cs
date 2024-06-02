using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class UserGroupMapper
    {
        public static UserGroupDTO toDTO (UserGroup userGroup,User? user,Group? group)
        {
            UserGroupDTO userGroupDTO = new UserGroupDTO
            {
                UserGroupId = userGroup.Id,
                GroupId = userGroup.GroupId,
                GroupName = group?.Name,
                GroupAcronym = group?.Acronym,
                UserId = userGroup.UserId,
                Username = user?.Username,
            };

            return userGroupDTO;
        }

        public static UserGroup toDAL(UserGroupDTO userGroupDTO)
        {
            UserGroup userGroup = new UserGroup
            {
                Id = userGroupDTO.UserGroupId,
                GroupId = userGroupDTO.GroupId,
                UserId = userGroupDTO.UserId
            };

            return userGroup;
        }
    }
}
