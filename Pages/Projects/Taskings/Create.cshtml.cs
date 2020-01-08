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
    public class CreateModel : DIBasePageModel
    {
        public CreateModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }
        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            Project = await Context.Project
                .FirstOrDefaultAsync(m => m.ProjectID == id);

            if (Project == null)
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

            ViewData["ProjectID"] = id;
            return Page();
        }

        [BindProperty]
        public Tasking Tasking { get; set; }

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

            Context.Tasking.Add(Tasking);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Projects/Details", new { id = id });
        }
    }
}
