using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public class DetailsModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public DetailsModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Blog Blog { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blog.FirstOrDefaultAsync(m => m.BlogID == id);
            //Blog = HttpUtility.HtmlEncode(Blog);

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {

            var blog = await _context.Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BlogID == id);

            Blog.BlogID = blog.BlogID;
            Blog.Title = "NICE";
            Blog.Author = blog.Author;
            Blog.Date = blog.Date;
            Blog.Post = blog.Post;
            Blog.Tags = blog.Tags;
            Blog.Likes = blog.Likes + 1;

            _context.Attach(Blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogID == id);
        }
    }
}
