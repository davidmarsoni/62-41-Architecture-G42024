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
    public class UsersController : ControllerBase
    {
        private readonly PrintOMatic_Context _context;

        public UsersController(PrintOMatic_Context context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();
            List<UserDTO> result = new List<UserDTO>();
            if (users != null && users.Count() > 0) {
                foreach ( User user in users )
                {
                    result.Add(UserMapper.toDTO(user));
                }
            }
            return result;
        }

        // GET: api/Users/NoAccount
        [HttpGet("NoAccount")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersNoAccount()
        {
            // get all users that do not have an account
            // first get all accounts users id
            IEnumerable<int> accountUsersId = await _context.Accounts.Select(a => a.UserId).ToListAsync();
            // second get all users that are not in the accountUsersId list and that are not deleted
            IEnumerable<User> users = await _context.Users.Where(u => !accountUsersId.Contains(u.Id) && !u.IsDeleted).ToListAsync();
            List<UserDTO> result = new List<UserDTO>();
            if (users != null && users.Count() > 0)
            {
                foreach (User user in users)
                {
                    result.Add(UserMapper.toDTO(user));
                }
            }
            return result;
        }

        // GET: api/Users/Active
        [HttpGet("Active")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersActive()
        {
            // get all users that are active
            IEnumerable<User> users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
            List<UserDTO> result = new List<UserDTO>();
            if (users != null && users.Count() > 0)
            {
                foreach (User user in users)
                {
                    result.Add(UserMapper.toDTO(user));
                }
            }
            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = UserMapper.toDTO(user);

            return userDTO;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.UserId)
            {
                return BadRequest();
            }

            User user;

            try
            {
                user = UserMapper.toDAL(userDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            User user;

            try
            {
                user = UserMapper.toDAL(userDTO);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
