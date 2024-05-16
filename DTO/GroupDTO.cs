namespace DTO
{
    public class GroupDTO
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string? Acronym { get; set; }

        // list of users
        public List<UserDTO> UserDTOs { get; set; }
    }
}
