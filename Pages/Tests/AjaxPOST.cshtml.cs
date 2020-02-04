using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public class AjaxPOSTModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public AjaxPOSTModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Blog Blog { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string Title)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Blog.Author = "AJAX";
            Blog.Title = Title;
            Blog.Date = DateTime.Now;
            Blog.Tags = "AJAX";
            Blog.Post = "AJAX";

            _context.Blog.Add(Blog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task OnPostTwoAsync(string Title)
        {
            
            Comment.Post = Title;
            Comment.Date = DateTime.Now;
            Comment.BlogID = 11;
            Comment.Author = "AJAX";

            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();
        }
    }
}
