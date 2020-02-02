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
    public class DeleteModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public DeleteModel(TaskManager.Data.ApplicationDbContext context)
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

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blog.FindAsync(id);

            if (Blog != null)
            {
                _context.Blog.Remove(Blog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { pageIndex = 1 });
        }
    }
}
