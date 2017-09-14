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
    [Route("api/ArticleTags")]
    public class ArticleTagsController : Controller
    {
        private readonly ApplicationContext _context;

        public ArticleTagsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/ArticleTags
        [HttpGet]
        public IEnumerable<ArticleTags> GetArticleTags()
        {
            return _context.ArticleTags;
        }

        // GET: api/ArticleTags/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleTags([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var articleTags = await _context.ArticleTags.SingleOrDefaultAsync(m => m.ArticleId == id);

            if (articleTags == null)
            {
                return NotFound();
            }

            return Ok(articleTags);
        }

        // PUT: api/ArticleTags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleTags([FromRoute] int id, [FromBody] ArticleTags articleTags)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articleTags.ArticleId)
            {
                return BadRequest();
            }

            _context.Entry(articleTags).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleTagsExists(id))
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

        // POST: api/ArticleTags
        [HttpPost]
        public async Task<IActionResult> PostArticleTags([FromBody] ArticleTags articleTags)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ArticleTags.Add(articleTags);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArticleTagsExists(articleTags.ArticleId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArticleTags", new { id = articleTags.ArticleId }, articleTags);
        }

        // DELETE: api/ArticleTags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleTags([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var articleTags = await _context.ArticleTags.SingleOrDefaultAsync(m => m.ArticleId == id);
            if (articleTags == null)
            {
                return NotFound();
            }

            _context.ArticleTags.Remove(articleTags);
            await _context.SaveChangesAsync();

            return Ok(articleTags);
        }

        private bool ArticleTagsExists(int id)
        {
            return _context.ArticleTags.Any(e => e.ArticleId == id);
        }
    }
}