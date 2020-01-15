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
    public class DeleteModel : DIBasePageModel
    {
        public DeleteModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Member Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Member = await Context.Member
                .Include(m => m.Project).FirstOrDefaultAsync(m => m.MemberID == id);

            //Owner and Member Authorization!
            var project = await Context
                .Project.AsNoTracking()
                //We are comparing the Member's copy of the ProjectID rather than relying on the address bar's ID
                .FirstOrDefaultAsync(p => p.ProjectID == Member.ProjectID);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
            var isAuthorized2 = false;
            var currentUserEmail = UserManager.GetUserName(User);
            if (currentUserEmail == Member.Email)
            {
                isAuthorized2 = true;
            }
            if (!isAuthorized.Succeeded)
            {
                if (isAuthorized2 == false)
                {
                    return Forbid();
                }
            }
            //------------



            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Member = await Context.Member
                .Include(m => m.Project).FirstOrDefaultAsync(m => m.MemberID == id);

            //Owner Authorization!
            var project = await Context
                .Project.AsNoTracking()
                //We are comparing the Member's copy of the ProjectID rather than relying on the address bar's ID
                .FirstOrDefaultAsync(p => p.ProjectID == Member.ProjectID);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Create);
            var isAuthorized2 = false;
            var currentUserEmail = UserManager.GetUserName(User);
            if (currentUserEmail == Member.Email)
            {
                isAuthorized2 = true;
            }
            if (!isAuthorized.Succeeded)
            {
                if (isAuthorized2 == false)
                {
                    return Forbid();
                }
            }
            //------------

            Member = await Context.Member.FindAsync(id);

            if (Member != null)
            {
                Context.Member.Remove(Member);
                await Context.SaveChangesAsync();
            }

            if (isAuthorized2 == true && !isAuthorized.Succeeded)
            {
                return RedirectToPage("/Projects/Index");
            }
            else
            {
                return RedirectToPage("/Projects/TaskManagement", new { id = project.ProjectID });
            }
        }
    }
}
