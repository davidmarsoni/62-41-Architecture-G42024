using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class TransactionHistoryManager
    {
        public static TransactionHistoryDTO toDTO(TransactionHistory transactionHistory)
        {
            TransactionHistoryDTO transactionHistoryDTO = new TransactionHistoryDTO
            {
                TransactionHistoryId = transactionHistory.Id,
                Amount = transactionHistory.Amount,
                DateTime = transactionHistory.DateTime,
                AccountId = transactionHistory.AccountId,
                Src = transactionHistory.Src,
                TransactionType = transactionHistory.TransactionType,
                ConversionName = transactionHistory.ConversionName,
                ConversionValue = transactionHistory.ConversionValue,
            };
            return transactionHistoryDTO;
        }
    }
}
