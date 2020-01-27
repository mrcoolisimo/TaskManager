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
using TaskManager.Models.BlogModels;
using TaskManager.Pages;
using PagedList;

namespace TaskManager
{
    public class DetailsModel : DIBasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public DetailsModel(ApplicationDbContext context,
                    IAuthorizationService authorizationService,
                    UserManager<IdentityUser> userManager)
                     : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public Blog Blog { get; set; }
        [BindProperty]
        public UserLike Likes { get; set; }
        [BindProperty]
        public Comment Comments { get; set; }
        public IList<Comment> CommentsList { get; set; }


        public async Task<IActionResult> OnGetAsync(int id, int page)
        {
            Blog = await Context.Blog.FirstOrDefaultAsync(m => m.BlogID == id);
            //Blog = HttpUtility.HtmlEncode(Blog);
            Likes = await Context.UserLike
                .AsNoTracking()
                .Where(l => l.BlogID == id)
                .FirstOrDefaultAsync(l => l.UserID == UserManager.GetUserId(User));
            CommentsList = await Context.Comments
                .Where(c => c.BlogID == id)
                .OrderByDescending(b => b.Date)
                .ToListAsync();
            //Comments = await Context.Comments.FirstOrDefaultAsync(c => c.BlogID == id);

            if (Likes == null)
            {
                ViewData["NoLikesYet"] = true;
            } else
            {
                ViewData["NoLikesYet"] = false;
            }

            if (Blog == null)
            {
                return NotFound();
            }
            

            return Page();
        }

        public async Task<IActionResult> OnGetLikesAsync(int data)
        {
            var blog = await Context.Blog
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.BlogID == data);
            return new JsonResult(blog.Likes);
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {

            var blog = await Context.Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BlogID == id);
            var userId = UserManager.GetUserId(User);
            var liked = await Context.UserLike
                .AsNoTracking()
                .Where(l => l.BlogID == id)
                .FirstOrDefaultAsync(l => l.UserID == userId) ;

            if (liked == null)
            {
                Blog.BlogID = blog.BlogID;
                Blog.Title = blog.Title;
                Blog.Author = blog.Author;
                Blog.Date = blog.Date;
                Blog.Post = blog.Post;
                Blog.Tags = blog.Tags;
                Blog.Likes = blog.Likes + 1;

                
                Likes.UserID = userId;
                Likes.BlogID = id;
                Likes.IsLiked = 1;
                //Likes.UserLikeID = 1;
                Context.UserLike.Add(Likes);

                Context.Attach(Blog).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return new JsonResult(Blog.Likes);
            } 
            else if (liked.IsLiked == 1)
            {
                Blog.BlogID = blog.BlogID;
                Blog.Title = blog.Title;
                Blog.Author = blog.Author;
                Blog.Date = blog.Date;
                Blog.Post = blog.Post;
                Blog.Tags = blog.Tags;
                Blog.Likes = blog.Likes - 1;

                Likes.UserID = userId;
                Likes.BlogID = id;
                Likes.UserLikeID = liked.UserLikeID;
                Likes.IsLiked = 0;

                Context.Attach(Blog).State = EntityState.Modified; 
                Context.Attach(Likes).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return new JsonResult(Blog.Likes);
            }
            else if (liked.IsLiked == 0)
            {
                Blog.BlogID = blog.BlogID;
                Blog.Title = blog.Title;
                Blog.Author = blog.Author;
                Blog.Date = blog.Date;
                Blog.Post = blog.Post;
                Blog.Tags = blog.Tags;
                Blog.Likes = blog.Likes + 1;

                Likes.UserID = userId;
                Likes.BlogID = id;
                Likes.UserLikeID = liked.UserLikeID;
                Likes.IsLiked = 1;

                Context.Attach(Blog).State = EntityState.Modified;
                Context.Attach(Likes).State = EntityState.Modified;
                await Context.SaveChangesAsync();

                return new JsonResult(Blog.Likes);
            }
            return new JsonResult(Blog.Likes);
        }
        public async Task<IActionResult> OnPostCommentAsync(int id)
        {
            var blog = await Context.Blog
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BlogID == id);
            var userId = UserManager.GetUserId(User);
            var username = UserManager.GetUserName(User);
            var pid = id;

            Comments.BlogID = blog.BlogID;
            Comments.Author = username;
            Comments.Post = HttpUtility.HtmlEncode(Comments.Post);
            Comments.Date = DateTime.Now;

            Context.Comments.Add(Comments);
            await Context.SaveChangesAsync();

            return RedirectToPage("/Blogs/Details", new { id = pid});
        }
    }
}
