using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Members
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
        public Member Member { get; set; }
        //public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Owner Authorization!
            Member = await Context.Member
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.MemberID == id);
            var project = await Context.Project
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == Member.ProjectID);
            var MyRole = await Context.Member
                .Include(m => m.Project)
                .AsNoTracking()
                .Where(m => m.ProjectID == project.ProjectID)
                .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded || MyRole.IsOwner != 1 || Member.Email == User.Identity.Name)
            {
                return Forbid();
            }
            //------------

            if (Member == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(Context.Project, "ProjectID", "ProjectID");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id, [Bind("IsOwner")] Member Member)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var member = await Context.Member
                .Include(m => m.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MemberID == id);

            //Owner Authorization!
            var project = await Context.Project
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == member.ProjectID);
            var MyRole = await Context.Member
                .Include(m => m.Project)
                .AsNoTracking()
                .Where(m => m.ProjectID == project.ProjectID)
                .FirstOrDefaultAsync(m => m.Email == User.Identity.Name);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded || MyRole.IsOwner != 1 || member.Email == User.Identity.Name)
            {
                return Forbid();
            }
            //------------

            Member.MemberID = member.MemberID;
            Member.ProjectID = member.ProjectID;
            Member.ProjectName = member.ProjectName;
            Member.Email = member.Email;
            Context.Attach(Member).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(Member.MemberID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Projects/Members/Details", new { id = Member.MemberID });
        }

        private bool MemberExists(int id)
        {
            return Context.Member.Any(e => e.MemberID == id);
        }
    }
}
