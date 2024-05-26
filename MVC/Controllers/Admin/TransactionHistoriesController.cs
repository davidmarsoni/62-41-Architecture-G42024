using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using MVC.Controllers.Util;
using MVC.Services.Interfaces;

namespace MVC.Controllers.Admin
{
    public class TransactionHistoriesController : Controller
    {
        private readonly ILogger<TransactionHistoriesController> _logger;
        private readonly ITransactionHistoryService _transactionHistoryService;

        public TransactionHistoriesController(ILogger<TransactionHistoriesController> logger, ITransactionHistoryService transactionHistoryService)
        {
            _logger = logger;
            _transactionHistoryService = transactionHistoryService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,Name,Acronym,IsDeleted")] TransactionHistoryDTO transactionHistory)
        {
            if (ModelState.IsValid)
            {
                if (await _transactionHistoryService.CreateTransactionHistory(transactionHistory) == null)
                {
                    ToastrUtil.ToastrError(this, "Unable to create group");
                    return View(transactionHistory);
                }
                // redirect to the new account page
                ToastrUtil.ToastrSuccess(this, "Transaction history successfully created");
                return RedirectToAction(nameof(Index));
            }
            return View(transactionHistory);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,Name,Acronym,IsDeleted")] TransactionHistoryDTO transactionHistory)
        {
            if (id != transactionHistory.TransactionHistoryId)
            {
                ToastrUtil.ToastrError(this, "An error has occured with the edit of groups, please contact support");
                return View(transactionHistory);
            }

            //remove the UserName from the model state
            if (ModelState.IsValid)
            {
                if (!await _transactionHistoryService.UpdateTransactionHistory(transactionHistory))
                {
                    ToastrUtil.ToastrError(this, "Transaction history update failed");
                    return RedirectToAction(nameof(Index));
                }
                ToastrUtil.ToastrSuccess(this, "Transaction history successfully updated");
                return RedirectToAction(nameof(Index));
            }

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
    }
}
