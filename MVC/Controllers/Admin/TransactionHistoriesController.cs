using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using MVC.Controllers.Util;
using MVC.Services.Interfaces;
using System.Text.Json.Nodes;
using Azure.Core.Serialization;

namespace MVC.Controllers.Admin
{
    public class TransactionHistoriesController : Controller
    {
        private readonly ILogger<TransactionHistoriesController> _logger;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IAccountService _accountService;
        private readonly IConversionService _conversionService;
        private IEnumerable<ConversionDTO>? _conversionDTOs;

        public TransactionHistoriesController(ILogger<TransactionHistoriesController> logger, ITransactionHistoryService transactionHistoryService, IAccountService accountService, IConversionService conversionService)
        {
            _logger = logger;
            _transactionHistoryService = transactionHistoryService;
            _accountService = accountService;
            _conversionService = conversionService;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            IEnumerable<TransactionHistoryDTO>? transactionHistories = await _transactionHistoryService.GetAllTransactionHistories();
            if (transactionHistories == null)
            {
                ToastrUtil.ToastrError(this, "Unable to fetch transactions, please contact support");
                return Redirect("/");
            }
            return View(transactionHistories);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var group = await _transactionHistoryService.GetTransactionHistoryById(id.Value);

            if (group == null)
            {
                return transactionHistoryNotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            await setupFields();
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId, Src, TransactionType, Amount, ConversionId")] TransactionHistoryDTO transactionHistory)
        {
            if (ModelState.IsValid)
            {
                // formalize the conversion
                await conversionFormalizationAsync(transactionHistory);

                if (await _transactionHistoryService.CreateTransactionHistory(transactionHistory) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create transactionHistory");
                    await setupFields();
                    return View(transactionHistory);
                }
                // redirect to the new account page
                ToastrUtil.ToastrSuccess(this, "Transaction history successfully created");
                return RedirectToAction(nameof(Index));
            }

            await setupFields();
            return View(transactionHistory);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var transactionHistory = await _transactionHistoryService.GetTransactionHistoryById(id.Value);

            if (transactionHistory == null)
            {
                return transactionHistoryNotFound();
            }

            await setupFields();
            return View(transactionHistory);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionHistoryId, AccountId, Src, TransactionType, Amount, ConversionId")] TransactionHistoryDTO transactionHistory)
        {
            if (id != transactionHistory.TransactionHistoryId)
            {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of transactionHistories, please contact support");
                return View(transactionHistory);
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                // formalize the conversion
                await conversionFormalizationAsync(transactionHistory);

                if (!await _transactionHistoryService.UpdateTransactionHistory(transactionHistory))
                {
                    ToastrUtil.ToastrError(this, "Transaction history update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "Transaction history successfully updated");
                return RedirectToAction(nameof(Index));
            }

            await setupFields();
            return View(transactionHistory);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return idNotProvided();
            }

            var group = await _transactionHistoryService.GetTransactionHistoryById(id.Value);

            if (group == null)
            {
                return transactionHistoryNotFound();
            }

            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _transactionHistoryService.DeleteTransactionHistory(id))
            {
                ToastrUtil.ToastrSuccess(this, "Transaction history successfully deleted");
                return RedirectToAction(nameof(Index));
            }
            ToastrUtil.ToastrError(this, "Transaction history deletion failed");
            return RedirectToAction(nameof(Delete), id);
        }

        private IActionResult idNotProvided()
        {
            ToastrUtil.ToastrError(this, "Id was not provided");
            return RedirectToAction(nameof(Index));
        }

        private IActionResult transactionHistoryNotFound()
        {
            ToastrUtil.ToastrError(this, "Transaction history not found");
            return RedirectToAction(nameof(Index));
        }

        #region Common Methods

        public async Task setupFields() { 
            await fetchAllAccountAsync();
            await fetchAllConversionsAsync();
            fetchSrcAndTransactionTypeSelects();
        }

        public async Task fetchAllAccountAsync()
        {
            IEnumerable<AccountDTO>? accounts = await _accountService.GetAllAccounts();
            ViewData["AccountsSelect"] = new SelectList(accounts, nameof(AccountDTO.UserId), nameof(AccountDTO.UserName));
            ViewData["AccountsAvailable"] = accounts?.Count() > 0;
        }

        public async Task fetchAllConversionsAsync()
        {
            // create the list and a empty conversion
            _conversionDTOs = new List<ConversionDTO>();
            _conversionDTOs = _conversionDTOs.Append(new ConversionDTO { ConversionId = -1, ConversionName = "None", ConversionValue = 0 });

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

        public void fetchSrcAndTransactionTypeSelects() {
            ViewData["SrcSelect"] = new SelectList(Enum.GetValues(typeof(DAL.Classes.Src)).Cast<DAL.Classes.Src>());
            ViewData["TransactionTypeSelect"] = new SelectList(Enum.GetValues(typeof(DAL.Classes.TransactionType)).Cast<DAL.Classes.TransactionType>());
        }

        public async Task conversionFormalizationAsync(TransactionHistoryDTO transactionHistoryDTO) {
            // if the conversionId is not null, then we need to add the conversionName and conversionValue to the transactionHistory
            if (transactionHistoryDTO.ConversionId != null && transactionHistoryDTO.ConversionId != -1)
            {
                IEnumerable<ConversionDTO>? conversions = await _conversionService.GetAllConversions(); 
                ConversionDTO? conversion = conversions?.FirstOrDefault(c => c.ConversionId == transactionHistoryDTO.ConversionId);
                if (conversion != null)
                {
                    transactionHistoryDTO.ConversionName = conversion.ConversionName;
                    transactionHistoryDTO.ConversionValue = conversion.ConversionValue;
                    return;
                }
            }
            transactionHistoryDTO.ConversionName = null;
            transactionHistoryDTO.ConversionValue = null;
        }

        #endregion
    }
}
