using DTO;

namespace MVC.Services.Interfacies
{
    public interface IAccountService
    {
        public AccountDTO GetAccountById(int id);
        public List<AccountDTO> GetAllAccounts();
        public AccountDTO CreateAccount(AccountDTO accountDTO);
        public AccountDTO UpdateAccount(AccountDTO accountDTO);
        public void DeleteAccount(int id);
    }
}
