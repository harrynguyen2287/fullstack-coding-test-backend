using Backend.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BlogController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> Get()
        {
            try
            {
                var blogs = await context.Blogs.ToListAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Blog>> Get(Guid id)
        {
            try
            {
                var blog = await context.Blogs.FindAsync(id);
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Save([FromBody] Blog blog)
        {
            try
            {
                var existBlog = await context.Blogs.FindAsync(blog.Id);

                if (existBlog != null)
                {
                    existBlog.Body = blog.Body;
                    existBlog.ImageAlt = blog.ImageAlt;
                    existBlog.ImageUrl = blog.ImageUrl;
                    existBlog.Title = blog.Title;
                }
                else
                {
                    var newBlog = new Blog
                    {
                        Id = Guid.NewGuid(),
                        Title = blog.Title,
                        Body = blog.Body,
                        ImageAlt = blog.ImageAlt,
                        ImageUrl = blog.ImageUrl,
                        CreatedDate = DateTime.UtcNow
                    };
                    context.Blogs.Add(newBlog);
                }

                await context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                var existBlog = await context.Blogs.FindAsync(id);

                if (existBlog != null)
                {
                    context.Blogs.Remove(existBlog);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}