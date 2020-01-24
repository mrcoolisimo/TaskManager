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
    public class IndexModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public IndexModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blog.OrderByDescending(b => b.Date).ToListAsync();
        }
    }
}
