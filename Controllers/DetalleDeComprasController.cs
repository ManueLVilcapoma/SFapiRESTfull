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
    public class DetalleDeComprasController : ControllerBase
    {
        private readonly FalabellaDbContext _context;

        public DetalleDeComprasController(FalabellaDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalleDeCompras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleDeCompra>>> GetDetalleDeCompras()
        {
          if (_context.DetalleDeCompras == null)
          {
              return NotFound();
          }
            return await _context.DetalleDeCompras.ToListAsync();
        }

        // GET: api/DetalleDeCompras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleDeCompra>> GetDetalleDeCompra(int id)
        {
          if (_context.DetalleDeCompras == null)
          {
              return NotFound();
          }
            var detalleDeCompra = await _context.DetalleDeCompras.FindAsync(id);

            if (detalleDeCompra == null)
            {
                return NotFound();
            }

            return detalleDeCompra;
        }

        // PUT: api/DetalleDeCompras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleDeCompra(int id, DetalleDeCompra detalleDeCompra)
        {
            if (id != detalleDeCompra.IdDetalleDeCompra)
            {
                return BadRequest();
            }

            _context.Entry(detalleDeCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleDeCompraExists(id))
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

        // POST: api/DetalleDeCompras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleDeCompra>> PostDetalleDeCompra(DetalleDeCompra detalleDeCompra)
        {
          if (_context.DetalleDeCompras == null)
          {
              return Problem("Entity set 'FalabellaDbContext.DetalleDeCompras'  is null.");
          }
            _context.DetalleDeCompras.Add(detalleDeCompra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetalleDeCompraExists(detalleDeCompra.IdDetalleDeCompra))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetalleDeCompra", new { id = detalleDeCompra.IdDetalleDeCompra }, detalleDeCompra);
        }

        // DELETE: api/DetalleDeCompras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleDeCompra(int id)
        {
            if (_context.DetalleDeCompras == null)
            {
                return NotFound();
            }
            var detalleDeCompra = await _context.DetalleDeCompras.FindAsync(id);
            if (detalleDeCompra == null)
            {
                return NotFound();
            }

            _context.DetalleDeCompras.Remove(detalleDeCompra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleDeCompraExists(int id)
        {
            return (_context.DetalleDeCompras?.Any(e => e.IdDetalleDeCompra == id)).GetValueOrDefault();
        }
    }
}
