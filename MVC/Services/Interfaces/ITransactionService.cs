using DTO;

namespace MVC.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<Boolean> PostTransaction(TransactionHistoryDTO transactionHistoryDTO);
    }
}
