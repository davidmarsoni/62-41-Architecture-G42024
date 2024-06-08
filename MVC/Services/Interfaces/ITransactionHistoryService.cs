using DTO;

namespace MVC.Services.Interfaces
{
    public interface ITransactionHistoryService
    {
        public Task<TransactionHistoryDTO?> GetTransactionHistoryById(int id);
        public Task<IEnumerable<TransactionHistoryDTO>?> GetAllTransactionHistories();
        Task<IEnumerable<TransactionHistoryDTO>?> GetTransactionHistoriesByAccountId(int accountId);
    }
}
