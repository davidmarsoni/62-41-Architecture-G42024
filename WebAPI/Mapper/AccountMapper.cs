using DTO;
using DAL.Models;

namespace WebApi.Mapper
{
    public class AccountMapper
    {
        public static AccountDTO toDTO (Account account, User? user)
        {
            AccountDTO accountDTO = new AccountDTO
            {
                AccountId = account.Id,
                Balance = account.Balance,
                UserId = account.UserId,
                UserName = user?.Username
            };
            return accountDTO;
        }

        public static Account toDAL (AccountDTO accountDTO)
        {
            Account account = new Account
            {
                Id = accountDTO.AccountId,
                Balance = accountDTO.Balance,
                UserId = accountDTO.UserId
            };
            return account;
        }

    }
}
