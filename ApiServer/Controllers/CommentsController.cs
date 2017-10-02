using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServer;
using ApiServer.Config;
using ApiServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Comments")]
    public class CommentsController : Controller
    {
        private readonly ApplicationContext _context;

        public CommentsController(ApplicationContext context)
        {
            _context = context;
        }


        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin+","+UserRoles.Moderator)]
        public async Task<IActionResult> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (comment.ArticleId == 0)
                return BadRequest();

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}