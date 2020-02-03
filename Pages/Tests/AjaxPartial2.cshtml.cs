using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Pages;

namespace TaskManager
{
    public class AjaxPartial2Model : DIBasePageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;
        public AjaxPartial2Model(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }
        public int Max { get; set; }
        [BindProperty]
        public Blog Blog { get; set; }
        public Comment Comments { get; set; }
        public void OnGet()
        {
            var count = Context.Comments.Count(t => t.BlogID == 11);
            Max = count / 10;
            if (count%10 != 0)
            {
                Max += 1;
            }
        }
        public async Task<IActionResult> OnPostAysnc(string comment)
        {
            var blog = await _context.Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BlogID == 11);
            var userId = UserManager.GetUserId(User);
            var username = UserManager.GetUserName(User);
            var pid = 11;

            Comments.BlogID = blog.BlogID;
            Comments.Author = username;
            Comments.Post = HttpUtility.HtmlEncode(comment);
            Comments.Date = DateTime.Now;

            _context.Comments.Add(Comments);
            await _context.SaveChangesAsync();

            return new JsonResult(comment);
        }
    }
}