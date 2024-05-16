using DAL.Classes;

namespace DTO
{
    public class TransactionHistoryDTO
    {
        public int TransactionHistoryId { get; set; }
        public int AccountId { get; set; }
        public DateTime DateTime { get; set; }
        public Src Src { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string? ConversionName { get; set; }
        public decimal? ConversionValue { get; set; }

    }
}
