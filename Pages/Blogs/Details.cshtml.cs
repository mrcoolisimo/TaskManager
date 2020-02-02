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
using PagedList;

namespace TaskManager
{
    public class DetailsModel : DIBasePageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? CurrentPage { get; set; }
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
        public Pagination<Comment> CommentsList { get; set; }

        public async Task<ActionResult> OnGetAsync(int id, int? pageIndex)
        {
            CurrentPage = pageIndex;
            if (pageIndex <= 0)
            {
                CurrentPage = 1;
            }

            Blog = await Context.Blog.FirstOrDefaultAsync(m => m.BlogID == id);
            //Blog = HttpUtility.HtmlEncode(Blog);
            Likes = await Context.UserLike
                .AsNoTracking()
                .Where(l => l.BlogID == id)
                .FirstOrDefaultAsync(l => l.UserID == UserManager.GetUserId(User));
            
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

            IQueryable<Comment> commentsIQ = from c in Context.Comments select c;

            int pageSize = 10;

            Comments = await Context.Comments.FirstOrDefaultAsync(c => c.BlogID == id);

            CommentsList = await Pagination<Comment>.CreateAsync(
                commentsIQ.Where(c => c.BlogID == id).OrderByDescending(c => c.Date).AsNoTracking(), pageIndex ?? 1, pageSize);

            return Page();
        }

        /*public async Task OnGetCommentsAsync(int page)
        {
            if (page < 1)
            {
                page = 1;
            }
            var pageSize = 2;
            var skip = pageSize * (page - 1);

            CommentsList2 = await Context.Comments
                .Where(c => c.BlogID == 11)
                .OrderByDescending(b => b.Date)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }*/

        public async Task<IActionResult> OnGetLikesAsync(int data)
        {
            var blog = await Context.Blog
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.BlogID == data);
            return new JsonResult(blog.Likes);
        }
        public async Task<IActionResult> OnPostAsync(int id, [Bind("")] UserLike Likes)
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
        public async Task<IActionResult> OnPostCommentAsync(int id, int? pageIndex, [Bind("Post")] Comment Comments)
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

            return RedirectToPage("/Blogs/Details", new { id = pid, pageIndex = 1 });
        }

        public async Task<IActionResult> OnPostCommentDeleteAsync(int id, int? pageIndex, int CommentId)
        {
            var pid = id;

            Comments = await Context.Comments.FirstOrDefaultAsync(c => c.CommentID == CommentId);

            if (Comments.Author == User.Identity.Name)
            {
                Context.Comments.Remove(Comments);
                await Context.SaveChangesAsync();
            }

            return RedirectToPage("/Blogs/Details", new { id = pid, pageIndex = 1 });
        }
    }
}
