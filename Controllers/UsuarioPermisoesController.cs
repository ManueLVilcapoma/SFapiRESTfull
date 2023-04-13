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
    public class UsuarioPermisoesController : ControllerBase
    {
        private readonly FalabellaDbContext _context;

        public UsuarioPermisoesController(FalabellaDbContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioPermisoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioPermiso>>> GetUsuarioPermisos()
        {
          if (_context.UsuarioPermisos == null)
          {
              return NotFound();
          }
            return await _context.UsuarioPermisos.ToListAsync();
        }

        // GET: api/UsuarioPermisoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioPermiso>> GetUsuarioPermiso(int id)
        {
          if (_context.UsuarioPermisos == null)
          {
              return NotFound();
          }
            var usuarioPermiso = await _context.UsuarioPermisos.FindAsync(id);

            if (usuarioPermiso == null)
            {
                return NotFound();
            }

            return usuarioPermiso;
        }

        // PUT: api/UsuarioPermisoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioPermiso(int id, UsuarioPermiso usuarioPermiso)
        {
            if (id != usuarioPermiso.IdUsuarioPermiso)
            {
                return BadRequest();
            }

            _context.Entry(usuarioPermiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioPermisoExists(id))
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

        // POST: api/UsuarioPermisoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioPermiso>> PostUsuarioPermiso(UsuarioPermiso usuarioPermiso)
        {
          if (_context.UsuarioPermisos == null)
          {
              return Problem("Entity set 'FalabellaDbContext.UsuarioPermisos'  is null.");
          }
            _context.UsuarioPermisos.Add(usuarioPermiso);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuarioPermisoExists(usuarioPermiso.IdUsuarioPermiso))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuarioPermiso", new { id = usuarioPermiso.IdUsuarioPermiso }, usuarioPermiso);
        }

        // DELETE: api/UsuarioPermisoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioPermiso(int id)
        {
            if (_context.UsuarioPermisos == null)
            {
                return NotFound();
            }
            var usuarioPermiso = await _context.UsuarioPermisos.FindAsync(id);
            if (usuarioPermiso == null)
            {
                return NotFound();
            }

            _context.UsuarioPermisos.Remove(usuarioPermiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioPermisoExists(int id)
        {
            return (_context.UsuarioPermisos?.Any(e => e.IdUsuarioPermiso == id)).GetValueOrDefault();
        }
    }
}
