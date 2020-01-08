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
    public class DetailsModel : DIBasePageModel
    {
        public DetailsModel(ApplicationDbContext context,
                     IAuthorizationService authorizationService,
                     UserManager<IdentityUser> userManager)
                      : base(context, authorizationService, userManager)
        {
        }

        public Tasking Tasking { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tasking = await Context.Tasking
                .Include(t => t.Project).FirstOrDefaultAsync(m => m.TaskingID == id);

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

            if (Tasking == null)
            {
                return NotFound();
            }

            //ViewData["ProjectID"] = Tasking.ProjectID;
            return Page();
        }
    }
}
