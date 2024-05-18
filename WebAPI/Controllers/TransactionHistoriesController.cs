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
                    result.Add(TransactionHistoryMapper.toDTO(transactionHistory));
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

            TransactionHistoryDTO transactionHistoryDTO = TransactionHistoryMapper.toDTO(transactionHistory);

            return transactionHistoryDTO;
        }

        // PUT: api/TransactionHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionHistory(int id, TransactionHistoryDTO transactionHistoryDTO)
        {
            if (id != transactionHistoryDTO.TransactionHistoryId)
            {
                return BadRequest();
            }

            TransactionHistory transactionHistory;

            try
            {
                transactionHistory = TransactionHistoryMapper.toDAL(transactionHistoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Entry(transactionHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TransactionHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionHistory>> PostTransactionHistory(TransactionHistoryDTO transactionHistoryDTO)
        {
            TransactionHistory transactionHistory;

            try
            {
                transactionHistory = TransactionHistoryMapper.toDAL(transactionHistoryDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.TransactionHistory.Add(transactionHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionHistory", new { id = transactionHistory.Id }, transactionHistory);
        }

        // DELETE: api/TransactionHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            var transactionHistory = await _context.TransactionHistory.FindAsync(id);
            if (transactionHistory == null)
            {
                return NotFound();
            }

            _context.TransactionHistory.Remove(transactionHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistory.Any(e => e.Id == id);
        }
    }
}
