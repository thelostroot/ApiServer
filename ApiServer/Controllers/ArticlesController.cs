using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServer;
using ApiServer.Config;
using ApiServer.Models;
using ApiServer.Proxies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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
        public ActionResult GetArticles()
        {
            var articles = _context.Articles
                .Include(x => x.ArticleTags).ThenInclude(at => at.Tag)
                .Include(x => x.Category);

            return Json(articles.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Text = x.ArticleText,
                PublishTime = x.PublishTime,
                Category = new
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name
                },
                Tags = x.ArticleTags.Select(at => new
                    {
                        Id = at.Tag.Id,
                        Name = at.Tag.Name
                    })
            }));
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
                .Include(x => x.Category)
                .SingleOrDefault(x => x.Id == id);

            if (article == null)
                return NotFound();
            

            return Ok(new
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.ArticleText,
                PublishTime = article.PublishTime,
                Category = new
                    {
                        Id = article.Category.Id,
                        Name = article.Category.Name
                    }, 
                Tags = article.ArticleTags.Select(at => new
                    {
                        Id = at.Tag.Id,
                        Name = at.Tag.Name
                    })
            });
        }

        // GET: api/Articles/5/full
        [HttpGet("{id}/full")]
        public ActionResult GetFullArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = _context.Articles
                .Include(x => x.ArticleTags).ThenInclude(at => at.Tag)
                .Include(x => x.Category)
                .Include(x => x.Comments).ThenInclude(com => com.User)
                .SingleOrDefault(m => m.Id == id);

            if (article == null)
                return NotFound();


            return Ok(new
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.ArticleText,
                PublishTime = article.PublishTime,
                Category = new
                {
                    Id = article.Category.Id,
                    Name = article.Category.Name
                },
                Comments = article.Comments.Select(com => new
                    {
                        Id = com.Id,
                        Text = com.Text,
                        User = new 
                            {
                                Id = com.User.Id,
                                Name = com.User.Name,
                                LastName = com.User.LastName,
                                Role = com.User.Role
                            }
                }),
                Tags = article.ArticleTags.Select(at => new
                {
                    Id = at.Tag.Id,
                    Name = at.Tag.Name
                })
            });
        }


        // TODO: Разобраться с возвращением результата
        // GET: api/Articles/5/Comments
        [HttpGet("{id}/Comments")]
        public ActionResult GetArticleComments([FromRoute] int id)
        {
            var article = _context.Articles
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);

            if (article != null)
            {
                return Json(article.Comments.Select( x => new
                {
                    Id = x.Id,
                    Text = x.Text,
                    User = new {
                        Name = x.User.Name,
                        LastName = x.User.LastName,
                        Role = x.User.Role
                    }
                }));
            }
            else
                return BadRequest("Статья не найдена!");
        }

        // TODO: Разобраться с возвращением результата
        // GET: api/Articles/5/Tags
        [HttpGet("{id}/Tags")]
        public ActionResult GetArticleTags([FromRoute] int id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);
            if (article != null)
            {
                var tags = _context.ArticleTags.Where(x => x.ArticleId == id).Include(x => x.Tag).Select(x => x.Tag).ToList();
                return Json(tags.Select(x => new 
                {
                    Id = x.Id,
                    Name = x.Name
                }));
            }
            else
                return BadRequest("Статья не найдена!");
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PostArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
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