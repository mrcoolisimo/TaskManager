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

namespace TaskManager.Pages.Projects.Members
{
    public class DetailsModel : DIBasePageModel
    {
        public DetailsModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }
        public Project Project { get; set; }
        public Member Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Member = await Context.Member
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.MemberID == id);
            Project = await Context.Project
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectID == Member.ProjectID);
            //Owner Authorization!
            var project = await Context
                //Include permitted roles
                .Project.Include(m => m.Members).AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == Member.ProjectID);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Read);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            if (id == null)
            {
                return NotFound();
            }

            Member = await Context.Member
                .Include(m => m.Project).FirstOrDefaultAsync(m => m.MemberID == id);

            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
