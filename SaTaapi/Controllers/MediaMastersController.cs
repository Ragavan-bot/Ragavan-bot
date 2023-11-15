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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MediaMastersController : ControllerBase
    {
        private readonly ClssContentSchedulerSTTContext _context;

        public MediaMastersController(ClssContentSchedulerSTTContext context)
        {
            _context = context;
        }

        // GET: api/MediaMasters
        [HttpPost("GetMediaMaster")]
        public async Task<ActionResult<IEnumerable<MediaMaster>>> GetMediaMasters()
        {
          if (_context.MediaMasters == null)
          {
              return NotFound();
          }
            return await _context.MediaMasters.ToListAsync();
        }

        // GET: api/MediaMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaMaster>> GetMediaMaster(int id)
        {
          if (_context.MediaMasters == null)
          {
              return NotFound();
          }
            var mediaMaster = await _context.MediaMasters.FindAsync(id);

            if (mediaMaster == null)
            {
                return NotFound();
            }

            return mediaMaster;
        }

        // PUT: api/MediaMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMediaMaster(int id, MediaMaster mediaMaster)
        {
            if (id != mediaMaster.MediaId)
            {
                return BadRequest();
            }

            _context.Entry(mediaMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaMasterExists(id))
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

        // POST: api/MediaMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MediaMaster>> PostMediaMaster(MediaMaster mediaMaster)
        {
          if (_context.MediaMasters == null)
          {
              return Problem("Entity set 'ClssContentSchedulerSTTContext.MediaMasters'  is null.");
          }
            _context.MediaMasters.Add(mediaMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMediaMaster", new { id = mediaMaster.MediaId }, mediaMaster);
        }

        // DELETE: api/MediaMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMediaMaster(int id)
        {
            if (_context.MediaMasters == null)
            {
                return NotFound();
            }
            var mediaMaster = await _context.MediaMasters.FindAsync(id);
            if (mediaMaster == null)
            {
                return NotFound();
            }

            _context.MediaMasters.Remove(mediaMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaMasterExists(int id)
        {
            return (_context.MediaMasters?.Any(e => e.MediaId == id)).GetValueOrDefault();
        }
    }
}
