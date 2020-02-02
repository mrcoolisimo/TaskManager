using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;

namespace TaskManager
{
    public class SearchModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public SearchModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Blog> Results { get; set; }
        public void OnGet()
        {
            Results = _context.Blog.ToList();
        }
    }
}