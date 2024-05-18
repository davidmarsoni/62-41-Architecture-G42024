using DTO;

namespace MVC.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountDTO> GetAccountById(int id);
        public Task<IEnumerable<AccountDTO>> GetAllAccounts();
        public Task<AccountDTO> CreateAccount(AccountDTO accountDTO);
        public Task<AccountDTO> UpdateAccount(AccountDTO accountDTO);
        public Task DeleteAccount(int id);
    }
}
