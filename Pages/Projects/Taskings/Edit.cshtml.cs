using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Taskings
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
        public Tasking Tasking { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tasking = await Context.Tasking
                .Include(t => t.Project).FirstOrDefaultAsync(m => m.TaskingID == id);

            if (Tasking == null)
            {
                return NotFound();
            }

            //Owner Authorization!
            var tasking = await Context
                .Tasking.Include(t => t.Project).AsNoTracking()
                .FirstOrDefaultAsync(p => p.TaskingID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, tasking,
                                                     Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            ViewData["ProjectID"] = Tasking.ProjectID;
            ViewData["TaskingID"] = Tasking.TaskingID;
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

            //Owner Authorization!
            var tasking = await Context
                .Tasking.Include(t => t.Project).AsNoTracking()
                .FirstOrDefaultAsync(p => p.TaskingID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, tasking,
                                                     Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            Context.Attach(Tasking).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskingExists(Tasking.TaskingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Projects/Details", new { id = id });
        }

        private bool TaskingExists(int id)
        {
            return Context.Tasking.Any(e => e.TaskingID == id);
        }
    }
}
