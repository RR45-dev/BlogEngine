using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Data;
using BlogEngine.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly BlogEngineContext _context;

        public AdminController(BlogEngineContext context)
        {
            _context = context;
        }

        // Admin Dashboard
        public async Task<IActionResult> Index()
        {
            var posts = await _context.BlogPosts.ToListAsync();
            return View(posts);
        }

        // Create a new Blog Post
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogPost post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedAt = DateTime.Now;
                _context.BlogPosts.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // Edit a Blog Post
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogPost post)
        {
            if (id != post.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // Delete a Blog Post
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) return NotFound();

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Approve or Delete Comments
        public async Task<IActionResult> ManageComments()
        {
            var comments = await _context.Comments.Include(c => c.BlogPost).ToListAsync();
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            comment.IsApproved = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageComments));
        }   

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageComments));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleApproval(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            comment.IsApproved = !comment.IsApproved; // Toggle status
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageComments));
        }

    }
}
