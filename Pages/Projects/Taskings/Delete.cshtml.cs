using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Taskings
{
    public class DeleteModel : DIBasePageModel
    {
        public DeleteModel(ApplicationDbContext context,
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

            //Project Owner Authorization!
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

            ViewData["TaskingID"] = Tasking.TaskingID;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tasking = await Context.Tasking.FindAsync(id);

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

            if (Tasking != null)
            {
                Context.Tasking.Remove(Tasking);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("/Projects/TaskManagement", new { id = Tasking.ProjectID });
        }
    }
}
