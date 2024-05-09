using DAL.Classes;

namespace DAL.Models
{
    public class TransactionHistory
    {
        public int TransactionHistoryId { get; set; }
        public Account Account { get; set; }
        public DateTime DateTime { get; set; }
        public Src Src { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string? ConversionName { get; set; }
        public decimal? ConversionValue { get; set; }
    }
}