using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLSSContentSchedulerSTT_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CLSSContentSchedulerSTT_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserChannelMapsController : ControllerBase
    {
        private readonly ClssContentSchedulerSTTContext _context;

        public UserChannelMapsController(ClssContentSchedulerSTTContext context)
        {
            _context = context;
        }

        [HttpPost("GetUserServerMap")]
        public async Task<IEnumerable<SPUserChannelMapResult>> GetUserServerMap(UserChannelMap userChannelMap)
        {
            return await _context.GetProcedures().SPUserChannelMapAsync(userChannelMap.UserId);
        }


        // GET: api/UserChannelMaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserChannelMap>>> GetUserChannelMaps()
        {
          if (_context.UserChannelMaps == null)
          {
              return NotFound();
          }
            return await _context.UserChannelMaps.ToListAsync();
        }

        // GET: api/UserChannelMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserChannelMap>> GetUserChannelMap(int id)
        {
          if (_context.UserChannelMaps == null)
          {
              return NotFound();
          }
            var userChannelMap = await _context.UserChannelMaps.FindAsync(id);

            if (userChannelMap == null)
            {
                return NotFound();
            }

            return userChannelMap;
        }

        // PUT: api/UserChannelMaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserChannelMap(int id, UserChannelMap userChannelMap)
        {
            if (id != userChannelMap.UsermapId)
            {
                return BadRequest();
            }

            _context.Entry(userChannelMap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserChannelMapExists(id))
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

        // POST: api/UserChannelMaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserChannelMap>> PostUserChannelMap(UserChannelMap userChannelMap)
        {
          if (_context.UserChannelMaps == null)
          {
              return Problem("Entity set 'ClssContentSchedulerSTTContext.UserChannelMaps'  is null.");
          }
            _context.UserChannelMaps.Add(userChannelMap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserChannelMap", new { id = userChannelMap.UsermapId }, userChannelMap);
        }

        // DELETE: api/UserChannelMaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserChannelMap(int id)
        {
            if (_context.UserChannelMaps == null)
            {
                return NotFound();
            }
            var userChannelMap = await _context.UserChannelMaps.FindAsync(id);
            if (userChannelMap == null)
            {
                return NotFound();
            }

            _context.UserChannelMaps.Remove(userChannelMap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserChannelMapExists(int id)
        {
            return (_context.UserChannelMaps?.Any(e => e.UsermapId == id)).GetValueOrDefault();
        }
    }
}
