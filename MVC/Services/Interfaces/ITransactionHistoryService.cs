using DTO;

namespace MVC.Services.Interfaces
{
    public interface ITransactionHistoryService
    {
        public List<TransactionHistoryDTO> GetTransactionHistories();
        public TransactionHistoryDTO GetTransactionHistory(int id);
        public TransactionHistoryDTO CreateTransactionHistory(TransactionHistoryDTO transactionHistoryDTO);
        public TransactionHistoryDTO UpdateTransactionHistory(int id, TransactionHistoryDTO transactionHistoryDTO);
        public TransactionHistoryDTO DeleteTransactionHistory(int id);
    }
}
