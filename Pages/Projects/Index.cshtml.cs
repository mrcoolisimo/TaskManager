using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IList<Member> Member { get; set; }

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

            //Find all affiliated projects
            var members = from m in Context.Member select m;
            var currentUserEmail = UserManager.GetUserName(User);
            members = members.Where(m => m.Email == currentUserEmail);
            members = members.Where(m => m.IsOwner != 1);

            //Output
            Project = await projects.ToListAsync();
            Member = await members.ToListAsync();
        }
    }
}