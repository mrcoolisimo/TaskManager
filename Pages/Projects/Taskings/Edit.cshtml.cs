using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Authorization;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Taskings
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
        public Tasking Tasking { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            Tasking = await Context.Tasking
                .Include(t => t.Project).FirstOrDefaultAsync(m => m.TaskingID == id);
            var project = await Context.Project
                .Include(t => t.Tasks)
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == Tasking.ProjectID);
            var members = await Context.Member.FirstOrDefaultAsync(m => m.ProjectID == Tasking.ProjectID);
            var member = await Context.Member.Where(m => m.Email == UserManager.GetUserName(User)).FirstOrDefaultAsync(m => m.ProjectID == Tasking.ProjectID);



            if (Tasking == null)
            {
                return NotFound();
            }
            //Authorization!
            var isAuthorized2 = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);

            if (!isAuthorized2.Succeeded)
            {
                return Forbid();
            }
            if (project.Owner != User.Identity.Name)
            {
                if (Tasking.Assignment != User.Identity.Name)
                {
                    //Do you not own this task?
                    if (Tasking.TaskOwnerName != User.Identity.Name)
                    {
                        //If youre not elevated
                        if (member.IsOwner != 2)
                        {
                            return Forbid();
                        }
                    }
                }
            }
            //------------

            

            ViewData["Members"] = new SelectList(Context.Set<Member>().Where(m => m.ProjectID == Tasking.ProjectID), "Email", "Email");
            ViewData["ProjectID"] = Tasking.ProjectID;
            ViewData["TaskingID"] = Tasking.TaskingID;
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var tasking = await Context
               .Tasking.Include(t => t.Project).AsNoTracking()
               .FirstOrDefaultAsync(p => p.TaskingID == id);
            var project = await Context.Project
                .Include(t => t.Tasks)
                .Include(m => m.Members)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == tasking.ProjectID);
            var member = await Context.Member
                .Where(m => m.Email == UserManager.GetUserName(User))
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProjectID == tasking.ProjectID);
            //Authorization!
            var isAuthorized2 = await AuthorizationService.AuthorizeAsync(
                                                     User, project,
                                                     Operations.Update);
            if (!isAuthorized2.Succeeded)
            {
                return Forbid();
            }
            if (project.Owner != User.Identity.Name)
            {
                if (Tasking.Assignment != User.Identity.Name)
                {
                    //Do you not own this task?
                    if (Tasking.TaskOwnerName != User.Identity.Name)
                    {
                        //If youre not elevated
                        if (member.IsOwner != 2)
                        {
                            return Forbid();
                        }
                    }
                }
            }
            //------------

            Tasking.TaskingID = tasking.TaskingID;
            Tasking.ProjectID = tasking.ProjectID;
            Tasking.TaskOwner = tasking.TaskOwner;
            Tasking.TaskOwnerName = tasking.TaskOwnerName;
            Context.Attach(Tasking).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskingExists(Tasking.TaskingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("/Projects/Taskings/Details", new { id = Tasking.TaskingID });
        }

        private bool TaskingExists(int id)
        {
            return Context.Tasking.Any(e => e.TaskingID == id);
        }
    }
}
