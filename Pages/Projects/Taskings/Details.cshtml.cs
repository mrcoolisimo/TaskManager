﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
            Tasking = await Context.Tasking
                .Include(t => t.Project).FirstOrDefaultAsync(m => m.TaskingID == id);

            //Owner Authorization!
            var tasking = await Context
                .Tasking
                .Include(t => t.Project)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TaskingID == id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, tasking,
                                                     Operations.Delete);

            //Member Authorization!
            var project = await Context
                //Include permitted roles
                .Project.Include(m => m.Members).AsNoTracking()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == tasking.ProjectID);
            var isAuthorizedMember = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                if (!isAuthorizedMember.Succeeded)
                {
                    return Forbid();
                }

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
