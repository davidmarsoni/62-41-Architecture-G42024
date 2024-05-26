using DTO;

namespace MVC.Services.Interfaces
{
    public interface ITransactionHistoryService
    {
        public Task<TransactionHistoryDTO?> GetTransactionHistoryById(int id);
        public Task<IEnumerable<TransactionHistoryDTO>?> GetAllTransactionHistories();
        public Task<TransactionHistoryDTO?> CreateTransactionHistory(TransactionHistoryDTO accountDTO);
        public Task<Boolean> UpdateTransactionHistory(TransactionHistoryDTO accountDTO);
        public Task<Boolean> DeleteTransactionHistory(int id);
    }
}
