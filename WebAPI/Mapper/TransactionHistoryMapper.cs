using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class TransactionHistoryMapper
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

        public static TransactionHistory toDAL(TransactionHistoryDTO transactionHistoryDTO)
        {
            TransactionHistory transactionHistory = new TransactionHistory
            {
                Id = transactionHistoryDTO.TransactionHistoryId,
                Amount = transactionHistoryDTO.Amount,
                DateTime = transactionHistoryDTO.DateTime,
                AccountId = transactionHistoryDTO.AccountId,
                Src = transactionHistoryDTO.Src,
                TransactionType = transactionHistoryDTO.TransactionType,
                ConversionName = transactionHistoryDTO.ConversionName,
                ConversionValue = transactionHistoryDTO.ConversionValue,
            };
            return transactionHistory;
        }
    }
}
