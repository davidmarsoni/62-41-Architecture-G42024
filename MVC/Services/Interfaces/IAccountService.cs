using DTO;

namespace MVC.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountDTO?> GetAccountById(int id);
        public Task<IEnumerable<AccountDTO>?> GetAllAccounts();
        public Task<AccountDTO?> GetAccountByUserId(int userId);
        public Task<AccountDTO?> CreateAccount(AccountDTO accountDTO);
        public Task<Boolean> UpdateAccount(AccountDTO accountDTO);
        public Task<Boolean> DeleteAccount(int id);
       
    }
}
