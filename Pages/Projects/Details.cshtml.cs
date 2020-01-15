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

namespace TaskManager.Pages.Projects
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //Owner Authorization!
            Project = await Context.Project
            .Include(t => t.Tasks)
            .Include(m => m.Members)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ProjectID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            //------------

            if (id == null)
            {
                return NotFound();
            }
            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
