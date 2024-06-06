using DTO;

namespace MVC.Models
{
    public class AppUserHistroyViewModel
    {
        public int UserId { get; set; }

        // user fields

        public string Username { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        //account fields
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        // history fields
        public List<TransactionHistoryDTO> TransactionHistories { get; set; }

        public string DisplayName => $"{FirstName} {LastName} ({Username})";
    }
}
