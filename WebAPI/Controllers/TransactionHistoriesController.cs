using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using WebApi.Mapper;
using WebApi.ExternalServices;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoriesController : ControllerBase
    {
        private readonly PrintOMatic_Context _context;

        public TransactionHistoriesController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: api/TransactionHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionHistoryDTO>>> GetTransactionHistory()
        {
            IEnumerable<TransactionHistory> transactionHistories = await _context.TransactionHistory.ToListAsync();
            List<TransactionHistoryDTO> result = new List<TransactionHistoryDTO>();
            if (transactionHistories != null && transactionHistories.Count() > 0)
            {
                foreach (TransactionHistory transactionHistory in transactionHistories)
                {
                    // find the user associated with the account
                    Account account = await _context.Accounts.FindAsync(transactionHistory.AccountId);
                    User user = await _context.Users.FindAsync(account.UserId);
                    // convert the transaction history to a DTO
                    result.Add(TransactionHistoryMapper.toDTO(transactionHistory, user));
                }
            }
            return result;
        }

        // GET: api/TransactionHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionHistoryDTO>> GetTransactionHistory(int id)
        {
            var transactionHistory = await _context.TransactionHistory.FindAsync(id);

            if (transactionHistory == null)
            {
                return NotFound();
            }

            // find the user associated with the account
            Account account = await _context.Accounts.FindAsync(transactionHistory.AccountId);
            User user = await _context.Users.FindAsync(account.UserId);
            // convert the transaction history to a DTO
            TransactionHistoryDTO transactionHistoryDTO = TransactionHistoryMapper.toDTO(transactionHistory, user);

            return transactionHistoryDTO;
        }

        // POST: api/TransactionHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionHistory>> PostTransactionHistory(TransactionHistoryDTO transactionHistoryDTO)
        {
            TransactionHistory transactionHistory;
            Account? account;

            // 1. check if the account exists
            account = await _context.Accounts.FindAsync(transactionHistoryDTO.AccountId);
            if (account == null)
            {
                return NotFound();
            }

            // 2. check if the conversion values are valid
            ActionResult result = (ActionResult) await ValidateConversion(transactionHistoryDTO);
            // check if the statuscode is not 200
            if (result.GetType() != typeof(OkResult))
            {
                return result;
            }

            try
            {
                transactionHistory = TransactionHistoryMapper.toDAL(transactionHistoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            // 3. beginning the transaction
            Decimal amount = transactionHistory.Amount;
            // dispatch the code treatment
            switch (transactionHistory.Src)
            {
                case DAL.Classes.Src.Printer:
                    // make sure the amount is negative
                    if (amount > 0)
                    {
                        // if the amount is positive, we just make it negative
                        amount = -amount;
                    }
                    // we check if the account has enough money
                    if (account.Balance + amount < 0)
                    {
                        return BadRequest();
                    }
                    // push the transaction to the printer server
                    PrinterSystemConnector printerSystemConnector = PrinterSystemConnector.getConnector();
                    if (!printerSystemConnector.IsConnected())
                    {
                        if (!await printerSystemConnector.ConnectToPrinterServer())
                        {
                            return StatusCode(500);
                        }
                    }
                    try
                    {
                        if (!await printerSystemConnector.PushTransactionOntoPrinterServer(transactionHistoryDTO))
                        {
                            throw new Exception("Unable to push transaction into print server");
                        }
                        // if the transaction is successful, we can continue and save the transaction into the database
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500);
                    }
                    break;
                default:
                    // all transaction that are not printer related are considered as a simple transaction
                    // check if the amount is positive for Src PayOnline, PaymentDB and Allocation (skip if the transactionType is CorrectCredit)
                    if (
                        transactionHistory.Src == DAL.Classes.Src.PayOnline 
                        || transactionHistory.Src == DAL.Classes.Src.PaymentDB
                        || (transactionHistory.Src == DAL.Classes.Src.Allocation
                            && transactionHistory.TransactionType != DAL.Classes.TransactionType.CorrectCredit)
                        )
                    {
                        if (amount < 0)
                        {
                            return BadRequest();
                        }
                    }
                    break;
            }
            // add/substract the amount from the account
            account.Balance += amount;
            _context.Entry(account).State = EntityState.Modified;

            transactionHistory.DateTime = DateTime.Now;
            _context.TransactionHistory.Add(transactionHistory);

            await _context.SaveChangesAsync();

            // if the transaction is successful, we can return the transaction history
            return CreatedAtAction("GetTransactionHistory", new { id = transactionHistory.Id }, transactionHistory);
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistory.Any(e => e.Id == id);
        }

        #region Common Methods

        private async Task<IActionResult> ValidateConversion(TransactionHistoryDTO transactionHistoryDTO)
        {
            // if conversion name is provided, then conversion value must be provided
            if (transactionHistoryDTO.ConversionName != null && transactionHistoryDTO.ConversionValue != null)
            {
                return Ok();
            }
            else if (transactionHistoryDTO.ConversionName == null && transactionHistoryDTO.ConversionValue == null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
