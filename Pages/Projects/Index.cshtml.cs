using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects
{
    public class IndexModel : DIBasePageModel
    {
        public IndexModel(ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<IdentityUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IList<Project> Project { get; set; }

        public async Task OnGetAsync()
        {
            //Check if they logged in
            if (!User.Identity.IsAuthenticated)
            {
                // They are not authenticated - send them away
                Response.Redirect("/Identity/Account/Login");
            }

            //Find all projects in our DB
            var projects = from p in Context.Project select p;

            var currentUserId = UserManager.GetUserId(User);

            projects = projects.Where(p => p.OwnerID == currentUserId);


            var loggedIn = UserManager.GetUserId(User) != null ? true : false;

            Project = await projects.ToListAsync();
        }
    }
}