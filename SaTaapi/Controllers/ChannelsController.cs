using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CLSSContentSchedulerSTT_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace CLSSContentSchedulerSTT_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly ClssContentSchedulerSTTContext _context;
        
        public ChannelsController(ClssContentSchedulerSTTContext context)
        {
            _context = context;
        }

        // GET: api/Channels
        [HttpPost("GetChannel")]
        public async Task<ActionResult<IEnumerable<Channel>>> GetChannels()
        {
          if (_context.Channels == null)
          {
              return NotFound();
          }
            return await _context.Channels.ToListAsync();
        }

        // GET: api/Channels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> GetChannel(int id)
        {
          if (_context.Channels == null)
          {
              return NotFound();
          }
            var channel = await _context.Channels.FindAsync(id);

            if (channel == null)
            {
                return NotFound();
            }

            return channel;
        }

        // PUT: api/Channels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutChannelCreation")]
        public async Task<IActionResult> PutChannel(Channel channel)
        {
            if (string.IsNullOrEmpty(channel.ChannelId.ToString()))
            {
                return BadRequest();
            }

            _context.Entry(channel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(channel.ChannelId))
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

        // POST: api/Channels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostChannelCreation")]
        public async Task<ActionResult<Channel>> PostChannel(Channel channel)
        {
          if (_context.Channels == null)
          {
              return Problem("Entity set 'ClssContentSchedulerSTTContext.Channels'  is null.");
          }
            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChannel", new { id = channel.ChannelId }, channel);
        }

        // DELETE: api/Channels/5
        [HttpDelete("DeleteChannelCreation")]
        public async Task<ActionResult<Channel>> DeleteChannel(Channel _channel)   
        {
            if (_context.Channels == null)
            {
                return NotFound();
            }
            var channel = await _context.Channels.FindAsync(_channel.ChannelId);
             
            if (channel == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChannelExists(int id)
        {
            return (_context.Channels?.Any(e => e.ChannelId == id)).GetValueOrDefault();
        }
        [HttpPost("ChanneltoUser")]
        public async Task<IEnumerable<SPChanneltoUserResult>> ChanneltoUser(Channel _channel)
        {
            return await _context.GetProcedures().SPChanneltoUserAsync(_channel.ChannelId);
        }


    }
}
