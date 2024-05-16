using System;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string? LastName { get; set; }
        public required string FirstName { get; set; }
        public string? Gender {get; set; }
        public string? Address { get; set; }
        public required string Email { get; set; }
        public bool IsDeleted { get; set; } = false!;

        public ICollection<Group> Groups { get;} = new List<Group>();
        public ICollection<User_Group> User_Groups { get; } = new List<User_Group>();
    }
}