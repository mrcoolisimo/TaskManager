using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Members
{
    public class CreateModel : DIBasePageModel
    {
        public CreateModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Owner Authorization!
            var project = await Context
                .Project.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            ViewData["ProjectID"] = id;

            ViewData["ProjectName"] = project.Title;

            return Page();
        }

        [BindProperty]
        public Member Member { get; set; }

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
                .Project.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            Member.ProjectID = id;
            Member.ProjectName = project.Title;
            Member.IsOwner = 0;
            Context.Member.Add(Member);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Projects/TaskManagement", new { id = project.ProjectID });
        }
    }
}
