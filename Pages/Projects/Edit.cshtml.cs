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

namespace TaskManager.Pages.Projects
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
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            /*if (id == null)
            {
                return NotFound();
            }*/

            Project = await Context.Project.FirstOrDefaultAsync(m => m.ProjectID == id);

            if (Project == null)
            {
                return NotFound();
            }

            //Owner Authorization!
            var project = await Context
                .Project
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            ViewData["ProjectID"] = id;
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            //Check if they logged in
            /*
            if (!User.Identity.IsAuthenticated)
            {
                //They are not authenticated - send them away
                return Forbid();
            }*/

            if (!ModelState.IsValid)
            {
                return Forbid();
            }

            // Fetch Contact from DB to get OwnerID.
            var project = await Context
                .Project
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
               return Forbid();
            }

            Project.OwnerID = UserManager.GetUserId(User);
            Project.Owner = UserManager.GetUserName(User);
            Context.Attach(Project).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(Project.ProjectID))
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

        private bool ProjectExists(int id)
        {
            return Context.Project.Any(e => e.ProjectID == id);
        }
    }
}
