using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;


namespace TaskManager.Pages.Projects
{
    public class TaskManagementModel : DIBasePageModel
    {
        public TaskManagementModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
             : base(context, authorizationService, userManager)
        {
        }
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Project = await Context.Project
                .Include(t => t.Tasks)
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == id);

            //Owner Authorization!
            /*
            var project = await Context
                .Project
                .Include(t => t.Tasks)
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProjectID == id);*/
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, Project,
                                                     Operations.Update);
            
            if (!isAuthorized.Succeeded)
            {
                    return Forbid();
            }
            
            //------------

            /*
            if (id == null)
            {
                return NotFound();
            }
            */
            

            if (Project == null)
            {
                return NotFound();
            }

            if (Project.Members.Count > 0)
            {
                ViewData["HasMembers"] = true;
            } else
            {
                ViewData["HasMembers"] = false;
            }

            return Page();
        }

        [BindProperty]
        public Member Member { get; set; }
        public async Task<IActionResult> OnPostAsync(int id)
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

            
            Member.ProjectID = id;
            Member.ProjectName = project.Title;
            Member.Email = project.Owner;
            Member.isOwner = 1;
            Context.Member.Add(Member);
            await Context.SaveChangesAsync();
            

            return RedirectToPage("/Projects/TaskManagement", new { id = project.ProjectID });
        }
    }
}
