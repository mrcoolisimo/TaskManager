using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
            var project = await Context
                   .Project.Include(m => m.Members).AsNoTracking()
                   .FirstOrDefaultAsync(p => p.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            //Owner Authorization!     
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
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
            var project = await Context
                .Project
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------
            Tasking.ProjectID = id;
            Tasking.TaskOwner = UserManager.GetUserId(User);
            Tasking.TaskOwnerName = UserManager.GetUserName(User);
            Context.Tasking.Add(Tasking);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Projects/TaskManagement", new { id = id });
        }
    }
}
