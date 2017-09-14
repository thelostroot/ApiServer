using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServer;
using ApiServer.Models;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Sources")]
    public class SourcesController : Controller
    {
        private readonly ApplicationContext _context;

        public SourcesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Sources
        [HttpGet]
        public IEnumerable<Source> GetSources()
        {
            return _context.Sources;
        }

        // GET: api/Sources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var source = await _context.Sources.SingleOrDefaultAsync(m => m.Id == id);

            if (source == null)
            {
                return NotFound();
            }

            return Ok(source);
        }

        // PUT: api/Sources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSource([FromRoute] int id, [FromBody] Source source)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != source.Id)
            {
                return BadRequest();
            }

            _context.Entry(source).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceExists(id))
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

        // POST: api/Sources
        [HttpPost]
        public async Task<IActionResult> PostSource([FromBody] Source source)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sources.Add(source);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSource", new { id = source.Id }, source);
        }

        // DELETE: api/Sources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSource([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var source = await _context.Sources.SingleOrDefaultAsync(m => m.Id == id);
            if (source == null)
            {
                return NotFound();
            }

            _context.Sources.Remove(source);
            await _context.SaveChangesAsync();

            return Ok(source);
        }

        private bool SourceExists(int id)
        {
            return _context.Sources.Any(e => e.Id == id);
        }
    }
}