using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AccountDTO
    {
        // account fields
        [Required(ErrorMessage = "AccountId is required")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Balance is required")]
        [Range(0, 1000, ErrorMessage = "Please enter a value between {1} and {2}")]
        public decimal Balance { get; set; }

        // user fields
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        
        public string? UserName { get; set; }
    }
}