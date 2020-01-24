using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager
{
    public class AjaxModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public AjaxModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            Blog = await _context.Blog.FirstOrDefaultAsync(m => m.BlogID == 11);
        }
        public async Task<IActionResult> OnGetDemo1Async()
        {
            var blog = await _context.Blog
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.BlogID == 11);
            //var likers = blog.Likes;
            return new JsonResult(blog.Likes);
            //return new JsonResult("Hello");
        }

        [BindProperty]
        public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetDemo2()
        {

            var blog = await _context.Blog
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.BlogID == 11);

            return new JsonResult("Hello");

            Blog.BlogID = blog.BlogID;

            Blog.Title = "NICE";
            Blog.Author = blog.Author;
            Blog.Date = blog.Date;
            Blog.Post = blog.Post;
            Blog.Tags = blog.Tags;
            Blog.Likes = blog.Likes + 1;
            _context.Attach(Blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new JsonResult("Hello");
        }
        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.BlogID == 11);
        }



        public async Task<ActionResult> OnPostAsync(string data)
        {
            //return new JsonResult("Received " + data + " at " + DateTime.Now);
            var blog = await _context.Blog
                               .AsNoTracking()
                               .FirstOrDefaultAsync(m => m.BlogID == 11);

            Blog.BlogID = blog.BlogID;

            Blog.Title = "NICE";
            Blog.Author = blog.Author;
            Blog.Date = blog.Date;
            Blog.Post = blog.Post;
            Blog.Tags = blog.Tags;
            Blog.Likes = blog.Likes + 1;
            _context.Attach(Blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}