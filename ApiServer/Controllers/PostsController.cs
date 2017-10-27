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
    [Route("api/Posts")]
    public class PostsController : Controller
    {
        private readonly ApplicationContext _context;

        public PostsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public ActionResult GetPosts()
        {
            var posts = _context.Posts
                .Include(x => x.PostTags).ThenInclude(at => at.Tag)
                .Include(x => x.Category);

            return Json(posts.Select(x => new PostProxy(x)));
        }
        

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public ActionResult GetPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _context.Posts
                .Include(x => x.PostTags).ThenInclude(at => at.Tag)
                .Include(x => x.Category)
                .SingleOrDefault(x => x.Id == id);

            if (post == null)
                return NotFound();
            

            return Ok(new PostProxy(post));
        }

        // GET: api/Articles/5/full
        [HttpGet("{id}/full")]
        public ActionResult GetFullPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = _context.Posts
                .Include(x => x.PostTags).ThenInclude(at => at.Tag)
                .Include(x => x.Category)
                .Include(x => x.Comments).ThenInclude(com => com.User)
                .SingleOrDefault(m => m.Id == id);

            if (post == null)
                return NotFound();


            return Ok(new PostFullProxy(post));
        }


        // TODO: Разобраться с возвращением результата
        // GET: api/Articles/5/Comments
        [HttpGet("{id}/Comments")]
        public ActionResult GetPostComments([FromRoute] int id)
        {
            var post = _context.Posts
                .Include(x => x.Comments).ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);

            if (post != null)
            {
                return Json(post.Comments.Select( x => new
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
        public ActionResult GetPostTags([FromRoute] int id)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                var tags = _context.PostTags.Where(x => x.PostId == id).Include(x => x.Tag).Select(x => x.Tag).ToList();
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
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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
        public async Task<IActionResult> PostPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}