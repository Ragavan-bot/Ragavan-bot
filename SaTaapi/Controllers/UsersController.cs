using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLSSContentSchedulerSTT_API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Channels;

namespace CLSSContentSchedulerSTT_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ClssContentSchedulerSTTContext _context;

        public UsersController(ClssContentSchedulerSTTContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpPost("GetUserMaster")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpPost("GetUserTypeMaster")]
        public async Task<IEnumerable<SPUserTypeMasterResult>> GetUserTypeMaster()
        {
            return await _context.GetProcedures().SPUserTypeMasterAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutUserCreation")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserId.ToString()))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
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
        [HttpPost("PostUserCreation")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ClssContentSchedulerSTTContext.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete(("DeleteUserCreation"))]
        public async Task<IActionResult> DeleteUser(User _user)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(_user.UserId);
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
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        [HttpPost("SPUserLogin")]
        public async Task<IEnumerable<SPUserValidationResult>> Login(User _user)
        {
            return await _context.GetProcedures().SPUserValidationAsync(_user.UserName, _user.Password);
        }
        [HttpPost("SPUserChannelPopup")]
        public async Task<IEnumerable<SPUserChannelPopupResult>>UserChannelPopup(User _user)
        {
            return await _context.GetProcedures().SPUserChannelPopupAsync(_user.UserId);
        }
        [HttpPost("SPUsertoChannel")]
        public async Task<IEnumerable<SPUsertoChannelResult>> SPUsertoChannel(User _user)
        {
            return await _context.GetProcedures().SPUsertoChannelAsync(_user.UserId);
        }
    }
}
