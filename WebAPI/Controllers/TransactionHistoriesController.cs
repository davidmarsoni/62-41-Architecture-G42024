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

    }
}
