using DAL.Classes;

namespace DAL.Models
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public Src Src { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; } = 0!;
        public string? ConversionName { get; set; }
        public decimal? ConversionValue { get; set; }
    }
}