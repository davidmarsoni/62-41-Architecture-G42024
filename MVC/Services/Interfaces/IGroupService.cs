using DTO;

namespace MVC.Services.Interfaces
{
    public interface IGroupService
    {
        public List<GroupDTO> GetGroups();
        public GroupDTO GetGroup(int id);
        public GroupDTO CreateGroup(GroupDTO groupDTO);
        public GroupDTO UpdateGroup(int id, GroupDTO groupDTO);
        public GroupDTO DeleteGroup(int id);
    }
}
