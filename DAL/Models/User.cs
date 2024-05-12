using System;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string? Gender {get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Group> Groups { get;} = new List<Group>();
        public ICollection<User_Group> User_Groups { get; } = new List<User_Group>();
    }
}