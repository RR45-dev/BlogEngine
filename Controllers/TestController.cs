using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Data;
using BlogEngine.Models;
using System.Linq;

public class TestController : Controller
{
    private readonly BlogEngineContext _context;

    public TestController(BlogEngineContext context)
    {
        _context = context;
    }

    public IActionResult GetAllPosts()
    {
        try
        {
            // Fetch all blog posts from the database
            List<BlogPost> posts = _context.BlogPosts.ToList();

            if (posts.Any())
            {
                return Json(posts); // Return data as JSON
            }

            return Content("No blog posts found.");
        }
        catch (Exception ex)
        {
            return Content($"Database Error: {ex.Message}");
        }
    }
}
