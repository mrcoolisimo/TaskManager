using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Pages;

namespace TaskManager
{
    public class EditModel : DIBasePageModel
    {
        public EditModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                        : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Blog = await Context.Blog.FirstOrDefaultAsync(m => m.BlogID == id);

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var blog = await Context.Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BlogID == id);

            Blog.Author = blog.Author;
            Blog.Tags = blog.Tags; 
            Blog.Date = blog.Date;
            Blog.Post = HttpUtility.HtmlEncode(Blog.Post);
            Blog.Likes = blog.Likes;

            Context.Attach(Blog).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(Blog.BlogID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BlogExists(int id)
        {
            return Context.Blog.Any(e => e.BlogID == id);
        }
    }
}
