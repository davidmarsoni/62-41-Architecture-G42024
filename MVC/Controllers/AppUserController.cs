using DAL.Classes;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Controllers.Util;
using MVC.Models;
using MVC.Services.Interfaces;
using System.Reflection;

namespace MVC.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ILogger<AppUserController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IAccountService _accountService;
        private readonly IConversionService _conversionService;
        private IEnumerable<ConversionDTO>? _conversionDTOs;
        private decimal _calculatedPrice;

        public AppUserController(ILogger<AppUserController> logger, ITransactionService transactionService, ITransactionHistoryService transactionHistoryService, IAccountService accountService, IConversionService conversionService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _transactionHistoryService = transactionHistoryService;
            _accountService = accountService;
            _conversionService = conversionService;
        }

        // GET: AppUser
        public IActionResult Index()
        {
            return View();
        }

        // GET: AppUser/Buy
        public async Task<IActionResult> Buy()
        {
            AppUserBuyViewModel appUserBuyViewModel = new AppUserBuyViewModel();
            await fetchAllAccountAsync();
            await createSelectListConversions();
            return View(appUserBuyViewModel);
        }

        // POST: AppUser/Buy
        [HttpPost]
        public async Task<IActionResult> Calculate([Bind("AccountId,ConversionId,Quantity")] AppUserBuyViewModel appUserBuyViewModel)
        {
            IEnumerable<ConversionDTO>? conversions = await fetchAllConversionDTOsAsync();
            appUserBuyViewModel.ConversionValue = 0;
            if (conversions != null && conversions.Any())
            {
                ConversionDTO? conversion = conversions.FirstOrDefault(c => c.ConversionId == appUserBuyViewModel.ConversionId);
                if (conversion == null)
                {
                    ToastrUtil.ToastrError(this, "Conversion not found");
                }
                else {
                    appUserBuyViewModel.ConversionValue = conversion.ConversionValue;
                }
            }
            await fetchAllAccountAsync();
            await createSelectListConversions();
            return View("Buy",appUserBuyViewModel);
        }

        // POST: AppUser/Buy
        [HttpPost]
        public async Task<IActionResult> Buy([Bind("AccountId,ConversionId,Quantity")] AppUserBuyViewModel appUserBuyViewModel, string command)
        {
            // remove the command from the model
            ModelState.Remove(nameof(command));
            if (ModelState.IsValid)
            {
                // fetch the conversion
                IEnumerable<ConversionDTO>? conversions = await fetchAllConversionDTOsAsync();
                ConversionDTO _conversion;
                appUserBuyViewModel.ConversionValue = 0;
                if (conversions != null && conversions.Any())
                {
                    ConversionDTO? conversion = conversions.FirstOrDefault(c => c.ConversionId == appUserBuyViewModel.ConversionId);
                    if (conversion == null)
                    {
                        ToastrUtil.ToastrError(this, "Conversion not found");
                        await fetchAllAccountAsync();
                        await createSelectListConversions();
                        return View();
                    }
                    else
                    {
                        _conversion = conversion;
                        appUserBuyViewModel.ConversionValue = conversion.ConversionValue;
                    }
                }
                else
                {
                    ToastrUtil.ToastrError(this, "Conversion not found");
                    await fetchAllAccountAsync();
                    await createSelectListConversions();
                    return View();
                }

                TransactionHistoryDTO transactionHistoryDTO = new TransactionHistoryDTO()
                {
                    AccountId = appUserBuyViewModel.AccountId,
                    TransactionType = TransactionType.UseCredit,
                    Src = Src.Printer,
                    Amount = appUserBuyViewModel.calculatedPrice,
                    ConversionName = _conversion.ConversionName,
                    ConversionValue = appUserBuyViewModel.ConversionValue
                };
                bool success = await _transactionService.PostTransaction(transactionHistoryDTO);
                if (success)
                {
                    ToastrUtil.ToastrSuccess(this, "Payment successful");
                }
                else
                {   
                    // get the account
                    AccountDTO? account = await _accountService.GetAccountById(appUserBuyViewModel.AccountId);
                    if (account == null)
                    {
                        ToastrUtil.ToastrError(this, "Account not found");
                        await fetchAllAccountAsync();
                        await createSelectListConversions();
                        return View();
                    }
                    // check if the account has enough balance
                    if (account.Balance < appUserBuyViewModel.calculatedPrice)
                    {
                        ToastrUtil.ToastrError(this, "Insufficient balance, you only have " + account.Balance + " CHF. (" + (account.Balance - appUserBuyViewModel.calculatedPrice) + " CHF)");
                    }
                    else
                    {
                        ToastrUtil.ToastrError(this, "Payment failed");
                    }
                }
            }
            await fetchAllAccountAsync();
            await createSelectListConversions();
            return View(appUserBuyViewModel);
        }

        public async Task<IActionResult> PayOnline()
        {
            await fetchAllAccountAsync();
            return View();
        }

        // POST: AppUser/PayOnline
        [HttpPost]
        public async Task<IActionResult> PayOnline([Bind("AccountId,Amount")] AppUserPayOnlineViewModel appUserPayOnlineViewModel)
        {
            if (ModelState.IsValid)
            {
                TransactionHistoryDTO transactionHistoryDTO = new TransactionHistoryDTO()
                {
                    AccountId = appUserPayOnlineViewModel.AccountId,
                    Amount = appUserPayOnlineViewModel.Amount,
                    TransactionType = TransactionType.AddCredit,
                    Src = Src.PayOnline
                };
                bool success = await _transactionService.PostTransaction(transactionHistoryDTO);
                if (success)
                {
                    ToastrUtil.ToastrSuccess(this, "Payment successful");
                }
                else
                {
                    ToastrUtil.ToastrError(this, "Payment failed");
                }
            }
            await fetchAllAccountAsync();
            return View();
        }

        // GET: AppUser/History
        public async Task<IActionResult> History()
        {
            await fetchAllAccountAsync();
            return View();
        }

        public async Task fetchAllAccountAsync()
        {
            IEnumerable<AccountDTO>? accounts = await _accountService.GetAllAccounts();
            ViewData["AccountsSelect"] = new SelectList(accounts, nameof(AccountDTO.AccountId), nameof(AccountDTO.UserDisplayName));
            ViewData["AccountsAvailable"] = accounts?.Count() > 0;
        }

        public async Task<IEnumerable<ConversionDTO>?> fetchAllConversionDTOsAsync()
        {
            return await _conversionService.GetAllConversions();
        }

        public async Task createSelectListConversions()
        {
            _conversionDTOs = await fetchAllConversionDTOsAsync();
            if (_conversionDTOs == null || _conversionDTOs.Count() == 0)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch conversions, please contact support");
            }
            // create the select list
            ViewData["ConversionsSelect"] = new SelectList(_conversionDTOs, nameof(ConversionDTO.ConversionId), nameof(ConversionDTO.ConversionName));
            ViewData["ConversionsAvailable"] = _conversionDTOs?.Count() > 0;
        }
    }
}
