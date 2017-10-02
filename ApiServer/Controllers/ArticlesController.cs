using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServer;
using ApiServer.Models;
using ApiServer.Proxies;

namespace ApiServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Articles")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationContext _context;

        public ArticlesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public IEnumerable<ArticleProxy> GetArticles()
        {
            var result = new List<ArticleProxy>();

            var articles = _context.Articles
                .Include(x => x.ArticleTags).ThenInclude(at => at.Tag)
                .Include(x => x.Comments).ThenInclude(c => c.User)
                .Include(x => x.Category)
                .Include(x => x.Source).ToList();

            foreach (var article in articles)
            {
                var articleProxy = new ArticleProxy(article);
                result.Add(articleProxy);
            }

            return result;
        }
        

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public ActionResult GetArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = _context.Articles
                .Include(x => x.ArticleTags).ThenInclude(at => at.Tag)
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Source)
                .SingleOrDefault(m => m.Id == id);

            if (article == null)
                return NotFound();

            var articleProxy = new ArticleProxy(article);

            return Ok(articleProxy);
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle([FromRoute] int id, [FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.Id)
            {
                return BadRequest();
            }

            if (article.CategoryId == 0 || article.SourceId == 0)
                return BadRequest();

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(article.CategoryId == 0 || article.SourceId == 0 )
                return BadRequest();

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return Ok(article);
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}