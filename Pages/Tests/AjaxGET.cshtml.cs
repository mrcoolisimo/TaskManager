using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public class Ajax2Model : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public Ajax2Model(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Blog Blog { get; set; }
        //public int Foo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            Blog = await _context.Blog.FirstOrDefaultAsync(m => m.BlogID == id);
            return Page();
        }
        public async Task<IActionResult> OnGetDemo4Async(int foo)
        {
            var blog = await _context.Blog
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.BlogID == foo);
            //var likers = blog.Likes;
            return new JsonResult(blog.Title);
            //return new JsonResult("Hello");
        }

    }
}
