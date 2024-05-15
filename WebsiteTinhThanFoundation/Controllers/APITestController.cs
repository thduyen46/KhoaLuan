using Microsoft.AspNetCore.Mvc;
using WebsiteTinhThanFoundation.Models;
using WebsiteTinhThanFoundation.Services;

namespace WebsiteTinhThanFoundation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APITestController : ControllerBase
    {
        private readonly BlogArticleService _blogArticleService;
        public APITestController(BlogArticleService blogArticleService)
        {
            _blogArticleService = blogArticleService;
        }


        [HttpPost("TestBlog")]    
        public async Task<IActionResult> TestBlog()
        {
            ICollection<Tag> listTag = new List<Tag>() { new Tag("A"), new Tag("B") };
            var blog = new BlogArticle()
            {
                Content = "test",
            };
            
            return NotFound();
        }
    }
}
