using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFapiRESTfull.Models;

namespace SFapiRESTfull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KardexesController : ControllerBase
    {
        private readonly FalabellaDbContext _context;

        public KardexesController(FalabellaDbContext context)
        {
            _context = context;
        }

        // GET: api/Kardexes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kardex>>> GetKardexs()
        {
          if (_context.Kardexs == null)
          {
              return NotFound();
          }
            return await _context.Kardexs.ToListAsync();
        }

        // GET: api/Kardexes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kardex>> GetKardex(int id)
        {
          if (_context.Kardexs == null)
          {
              return NotFound();
          }
            var kardex = await _context.Kardexs.FindAsync(id);

            if (kardex == null)
            {
                return NotFound();
            }

            return kardex;
        }

        // PUT: api/Kardexes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKardex(int id, Kardex kardex)
        {
            if (id != kardex.IdKardex)
            {
                return BadRequest();
            }

            _context.Entry(kardex).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KardexExists(id))
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

        // POST: api/Kardexes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kardex>> PostKardex(Kardex kardex)
        {
          if (_context.Kardexs == null)
          {
              return Problem("Entity set 'FalabellaDbContext.Kardexs'  is null.");
          }
            _context.Kardexs.Add(kardex);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KardexExists(kardex.IdKardex))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKardex", new { id = kardex.IdKardex }, kardex);
        }

        // DELETE: api/Kardexes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKardex(int id)
        {
            if (_context.Kardexs == null)
            {
                return NotFound();
            }
            var kardex = await _context.Kardexs.FindAsync(id);
            if (kardex == null)
            {
                return NotFound();
            }

            _context.Kardexs.Remove(kardex);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KardexExists(int id)
        {
            return (_context.Kardexs?.Any(e => e.IdKardex == id)).GetValueOrDefault();
        }
    }
}
