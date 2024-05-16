﻿namespace DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        // list of groups
        public List<GroupDTO> GroupDTOs { get; set; }

    }
}
