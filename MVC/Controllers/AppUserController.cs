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
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IAccountService _accountService;
        private readonly IConversionService _conversionService;
        private IEnumerable<ConversionDTO>? _conversionDTOs;
        private decimal _calculatedPrice;

        public AppUserController(ILogger<AppUserController> logger, ITransactionHistoryService transactionHistoryService, IAccountService accountService, IConversionService conversionService)
        {
            _logger = logger;
            _transactionHistoryService = transactionHistoryService;
            _accountService = accountService;
            _conversionService = conversionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PayOnline()
        {
            return View();
        }


        public async Task<IActionResult> Buy()
        {
            AppUserBuyViewModel appUserBuyViewModel = new AppUserBuyViewModel();
            await fetchAllAccountAsync();
            await createSelectListConversions();
            return View(appUserBuyViewModel);
        }

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

        [HttpPost]
        public async Task<IActionResult> Buy([Bind("AccountId,ConversionId,Quantity")] AppUserBuyViewModel appUserBuyViewModel, string command)
        {
            AppUserBuyViewModel newAppUserBuyViewModel = new AppUserBuyViewModel();
            await fetchAllAccountAsync();
            await createSelectListConversions();
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
