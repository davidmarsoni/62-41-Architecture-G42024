using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required (ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string Username { get; set; }

        [StringLength(80, ErrorMessage = "Last name cannot exceed 80 characters")]
        public string? LastName { get; set; }

        [Required (ErrorMessage = "First name is required")]
        [StringLength(80, ErrorMessage = "First name cannot exceed 80 characters")]
        public string FirstName { get; set; }

        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
        public string? Gender { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(200, ErrorMessage = "Email cannot exceed 200 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "IsDeleted is required")]
        public bool IsDeleted { get; set; }

        public string DisplayName => $"{FirstName} {LastName} ({Email})";
    }
}