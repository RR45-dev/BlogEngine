using BlogEngine.Data;
using BlogEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogEngineContext _context;

        public BlogController(BlogEngineContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch all blog posts
            var posts = _context.BlogPosts.ToList();
            return View(posts); // Pass the posts to the view
        }

        public IActionResult Details(int id)
        {
            // Fetch a single blog post by Id
            var post = _context.BlogPosts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();
            return View(post); // Pass the post to the details view
        }


        [HttpPost]
        public IActionResult AddComment(int postId, string Author, string Text)
        {
            if (string.IsNullOrWhiteSpace(Author) || string.IsNullOrWhiteSpace(Text))
            {
                ModelState.AddModelError(string.Empty, "Both Name and Comment fields are required.");
                return RedirectToAction("Details", new { id = postId });
            }

            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
            {
                return NotFound("Blog post not found.");
            }

            var comment = new Comment
            {
                BlogPostId = postId,
                Author = Author,
                Text = Text,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = postId });
        }


        // Add additional actions like Create, Edit, Delete, etc.
    }
}
