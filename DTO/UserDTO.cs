using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsDeleted { get; set; }
    }
}