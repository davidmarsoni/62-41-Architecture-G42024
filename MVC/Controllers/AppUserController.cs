using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Services.Interfaces;

namespace MVC.Controllers
{
    public class AppUserController : Controller
    {
        private readonly ILogger<AppUserController> _logger;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IAccountService _accountService;
        private readonly IConversionService _conversionService;
        private IEnumerable<ConversionDTO>? _conversionDTOs;

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
            await fetchAllAccountAsync();
            await fetchAllConversionsAsync();
            return View();
        }

        public async Task fetchAllAccountAsync()
        {
            IEnumerable<AccountDTO>? accounts = await _accountService.GetAllAccounts();
            ViewData["AccountsSelect"] = new SelectList(accounts, nameof(AccountDTO.AccountId), nameof(AccountDTO.UserName));
            ViewData["AccountsAvailable"] = accounts?.Count() > 0;
        }

        public async Task fetchAllConversionsAsync()
        {
            _conversionDTOs = new List<ConversionDTO>();
            // fetch all the conversions and add them to the list
            IEnumerable<ConversionDTO>? conversions = await _conversionService.GetAllConversions();
            if (conversions != null)
            {
                _conversionDTOs = _conversionDTOs.Concat(conversions);
            }

            // create the select list
            ViewData["ConversionsSelect"] = new SelectList(_conversionDTOs, nameof(ConversionDTO.ConversionId), nameof(ConversionDTO.ConversionName));
            ViewData["ConversionsAvailable"] = _conversionDTOs?.Count() > 0;
        }
    }
}
