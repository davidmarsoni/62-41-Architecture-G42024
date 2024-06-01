using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class TransactionHistoryMapper
    {
        public static TransactionHistoryDTO toDTO(TransactionHistory transactionHistory, User? user)
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
                AccountUsername = user?.Username
            };
            return transactionHistoryDTO;
        }

        public static TransactionHistory toDAL(TransactionHistoryDTO transactionHistoryDTO)
        {
            TransactionHistory transactionHistory = new TransactionHistory
            {
                Id = transactionHistoryDTO.TransactionHistoryId,
                Amount = transactionHistoryDTO.Amount,
                DateTime = (DateTime)(transactionHistoryDTO.DateTime == null ? DateTime.Now : transactionHistoryDTO.DateTime),
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
