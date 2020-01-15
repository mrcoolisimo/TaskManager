using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects
{
    public class CreateModel : DIBasePageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
             : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }
        public Member Member { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /*
            //Check if they logged in
            if (!User.Identity.IsAuthenticated)
            {
                // They are not authenticated - send them away
                Response.Redirect("/Identity/Account/Login");
            }
            */

            Project.OwnerID = UserManager.GetUserId(User);
            Project.Owner = UserManager.GetUserName(User);

            Context.Project.Add(Project);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
