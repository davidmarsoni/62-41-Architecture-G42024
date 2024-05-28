using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Services.Interfaces;
using DTO;
using MVC.Controllers.Util;

namespace MVC.Controllers.Admin
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountsController(ILogger<AccountsController> logger, IAccountService accountService, IUserService userService)
        {
            _logger = logger;
            _accountService = accountService;
            _userService = userService;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            IEnumerable<AccountDTO>? accounts = await _accountService.GetAllAccounts();
            if (accounts == null) {
                ToastrUtil.ToastrError(this, "Unable to fetch accounts, please contact support");
                return Redirect("/");
            }
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided(); 
            }

            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return accountNotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public async Task<IActionResult> CreateAsync()
        {
            // fetch the available users
            await fetchAllUsersAsync();
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Balance")] AccountDTO account)
        {
            // remove the UserName from the model state
            ModelState.Remove(nameof(UserDTO.Username));
            if (ModelState.IsValid)
            {
                if (await _accountService.CreateAccount(account) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create account");
                    return View(account);
                }
                // redirect to the new account page
                ToastrUtil.ToastrSuccess(this, "Account successfully created");
                return RedirectToAction(nameof(Index));
            }

            // fetch the available users
            await fetchAllUsersAsync();
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return accountNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,UserId,Balance")] AccountDTO account)
        {
            if (id != account.AccountId) {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of accounts, please contact support");
                return View(account);
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                if (!await _accountService.UpdateAccount(account))
                {
                    ToastrUtil.ToastrError(this, "Account update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "Account successfully updated");
                return RedirectToAction(nameof(Index));
            }

            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }
            
            var account = await _accountService.GetAccountById(id.Value);

            if (account == null)
            {
                return accountNotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _accountService.DeleteAccount(id))
            {
                ToastrUtil.ToastrSuccess(this, "Account successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrError(this, "Account deletion failed");
            return RedirectToAction(nameof(Delete), id);
        }

        private IActionResult idNotProvided() {
            ToastrUtil.ToastrError(this, "Id was not provided");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult accountNotFound() {
            ToastrUtil.ToastrError(this, "Account not found");
            return RedirectToAction(nameof(Index));
        }

        #region Common Methods

        public async Task fetchAllUsersAsync()
        {
            IEnumerable<UserDTO>? users = await _userService.GetAllUsersActiveWithoutAccount();
            ViewData["UsersSelect"] = new SelectList(users, nameof(UserDTO.UserId), nameof(UserDTO.Username));
            ViewData["UsersAvailable"] = users?.Count() > 0;
        }

        #endregion
    }
}
