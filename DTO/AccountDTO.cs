namespace DTO
{
    public class AccountDTO
    {
        // account fields
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        // user fields
        public int UserId { get; set; }

        public string? UserName { get; set; }
    }
}
