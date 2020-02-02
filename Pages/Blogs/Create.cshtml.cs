using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public class CreateModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public CreateModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Blog Blog { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync([Bind("Title,Tags,Post")] Blog Blog)
        {
            if (Blog.Title == null || Blog.Post == null)
            {
                return Page();
            }
            if (Blog.Tags == null)
            {
                Blog.Tags = "";
            }

            Blog.Author = User.Identity.Name;
            Blog.Date = DateTime.Now;
            Blog.Tags = Blog.Tags.ToUpper();
            Blog.Post = HttpUtility.HtmlEncode(Blog.Post);

            _context.Blog.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { pageIndex = 1, id = Blog.BlogID });
        }
    }
}
