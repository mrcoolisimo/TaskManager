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

        [BindProperty(SupportsGet = true)]
        public int? CurrentPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public string CurrentFilter { get; set; }
        public IList<Blog> Blog { get;set; }
        public Pagination<Blog> BlogP { get; set; }

        public async Task OnGetAsync(int? pageIndex, string searchString, string currentFilter)
        {
            CurrentPage = pageIndex;
            CurrentFilter = searchString;

            if (pageIndex <= 0)
            {
                CurrentPage = 1;
            }

            Blog = await _context.Blog.OrderByDescending(b => b.Date).ToListAsync();

            var pageSize = 10;
            IQueryable<Blog> blogIQ = from b in _context.Blog select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                blogIQ = blogIQ.Where(s => s.Author.Contains(searchString)
                                       || s.Tags.Contains(searchString)
                                       || s.Post.Contains(searchString));
            }
            BlogP = await Pagination<Blog>.CreateAsync(
               blogIQ.OrderByDescending(c => c.Date).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
