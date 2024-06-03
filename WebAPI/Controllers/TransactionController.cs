using DAL;
using DAL.Classes;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.ExternalServices;
using WebApi.Mapper;

namespace WebApi.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly PrintOMatic_Context _context;

        public TransactionController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // POST: api/Accounts/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Account")]
        public async Task<ActionResult> AddTransaction(TransactionHistoryDTO transactionHistoryDTO)
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
            ActionResult result = (ActionResult)await ValidateConversion(transactionHistoryDTO);
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
                return StatusCode(500, "An error occurred while processing the transaction."+ e.Message);
            }

            // 3. beginning the transaction
            
            Decimal amount = transactionHistory.Amount;

            // Test if the user have enought money to do the transaction
            if (transactionHistory.TransactionType == TransactionType.UseCredit)
            {
                // check if the amount is positive
                if(amount < 0)
                {
                    return BadRequest("The amount must be positive for a use credit transaction.");
                }

                // check if the account has enough money
                if (account.Balance - amount < 0)
                {
                    return BadRequest("The account does not have enough balance for this transaction.");
                }

            }

            // Call the printer if the transaction is a use credit
            if (transactionHistory.Src == Src.Printer && transactionHistory.TransactionType == TransactionType.UseCredit)
            {
                //send the transaction to the printer
                PrinterSystemConnector printerSystemConnector = PrinterSystemConnector.getConnector();

                if (!printerSystemConnector.IsConnected())
                {
                    if (!await printerSystemConnector.ConnectToPrinterServer())
                    {
                        return StatusCode(500, "An error occurred while connecting to the printer server.");
                    }
                }

                try
                {
                    //TODO correct add conversion to pages and send to printer
                    int pages = (int)Math.Floor((decimal)(transactionHistory.Amount / transactionHistory.ConversionValue));

                    if (!await printerSystemConnector.PushTransactionOntoPrinterServer(transactionHistory.ConversionName,pages))
                    {
                        throw new Exception("Unable to push transaction into print server");
                    }
                    // if the transaction is successful, we can continue and save the transaction into the database
                }
                catch (Exception e)
                {
                    return StatusCode(500, "An error occurred while processing the transaction." + e.Message);
                }

            }

            // add/substract the amount from the account
            if(transactionHistory.TransactionType == TransactionType.AddCredit)
            {
                account.Balance += amount;
            }
            else if(transactionHistory.TransactionType == TransactionType.UseCredit)
            {
                account.Balance -= amount;
            }
            else if(transactionHistory.TransactionType == TransactionType.CorrectCredit )
            {
                account.Balance += amount;
            }
           
            // if the balance is negative, set it to 0
            if(account.Balance < 0)
            {
               account.Balance = 0;
            }

            _context.Entry(account).State = EntityState.Modified;

            transactionHistory.DateTime = DateTime.Now;
            _context.TransactionHistory.Add(transactionHistory);

            await _context.SaveChangesAsync();

            return Ok();
        }

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
                return BadRequest("Conversion name and value must be provided together. and be valid");
            }
        }
    }

}
