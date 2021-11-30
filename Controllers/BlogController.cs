using Backend.Entities;
using Backend.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<bool>> Save(Blog blog)
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