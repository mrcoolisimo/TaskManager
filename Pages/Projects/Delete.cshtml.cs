using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects
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
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // They are not authenticated - send them away
                Response.Redirect("/Identity/Account/Login");
            }

            Project = await Context.Project.AsNoTracking().FirstOrDefaultAsync(m => m.ProjectID == id);

            if (Project == null)
            {
                return NotFound();
            }

            //Owner Authorization!
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            //Owner Authorization!
            var project = await Context
                .Project.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            Project = await Context.Project.FindAsync(id);

            if (Project != null)
            {
                Context.Project.Remove(Project);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
