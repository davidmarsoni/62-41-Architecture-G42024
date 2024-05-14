using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PrintSystem : ControllerBase
{
    private readonly PrintOMatic_Context _context;

    public PrintSystem(PrintOMatic_Context context)
    {
        _context = context;
    }

    // GET: api/MyClass
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> getUsers() { 
        return await _context.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }


    // Other methods for POST, PUT, DELETE, etc. would go here
}