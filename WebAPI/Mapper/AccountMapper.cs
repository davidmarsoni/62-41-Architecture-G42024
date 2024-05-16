using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class AccountMapper
    {
        public static AccountDTO toDTO (Account account)
        {
            AccountDTO accountDTO = new AccountDTO();
            accountDTO.AccountId = account.Id;
            accountDTO.Balance = account.Balance;
            accountDTO.UserId = account.Id;
            return accountDTO;
        }

        public static Account toDAL (AccountDTO accountDTO)
        {
            Account account = new Account();
            account.Id = accountDTO.AccountId;
            account.Balance = accountDTO.Balance;      
            account.UserId = accountDTO.UserId;
            return account;
        }

    }
}
