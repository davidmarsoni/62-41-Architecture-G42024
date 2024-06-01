using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using DTO;
using WebApi.Mapper;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly PrintOMatic_Context _context;

        public AccountsController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<List<AccountDTO>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _context.Accounts.ToListAsync();
            List<AccountDTO> result = new List<AccountDTO>();
            if (accounts != null && accounts.Count() > 0)
            {
                foreach (Account account in accounts)
                {
                    // get the user associated with the account
                    User user = await _context.Users.FindAsync(account.UserId);
                    result.Add(AccountMapper.toDTO(account, user));
                }
            }
            return result;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDTO>> GetAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            // get the user associated with the account
            User user = await _context.Users.FindAsync(account.UserId);

            AccountDTO accountDTO = AccountMapper.toDTO(account, user);

            return accountDTO;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountDTO accountDTO)
        {
            if (id != accountDTO.AccountId)
            {
                return BadRequest();
            }

            if (!Validator.TryValidateObject(accountDTO, new ValidationContext(accountDTO), null, true))
            {
                return BadRequest();
            }

            Account account;

            try
            {
                account = AccountMapper.toDAL(accountDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> PostUser(AccountDTO accountDTO)
        {
            Account account;
            if (accountDTO == null)
            {
                   return BadRequest();
            }
            // check if the user exists
            if (!_context.Users.Any(e => e.Id == accountDTO.UserId))
            {
                return NotFound();
            }
            // check if an account already exist for the user
            if (_context.Accounts.Any(e => e.UserId == accountDTO.UserId))
            {
                return Conflict();
            }
            try
            {
                account = AccountMapper.toDAL(accountDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
