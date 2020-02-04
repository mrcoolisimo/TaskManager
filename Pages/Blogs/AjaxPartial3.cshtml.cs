using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager
{
    public class AjaxPartial3Model : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public AjaxPartial3Model(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Comment Comments { get; set; }
        public int? CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public Pagination<Comment> CommentsList { get; set; }

        public async Task OnGetAsync(int? pageIndex,int id)
        {
            var count = (from o in _context.Comments
                         where o.CommentID == id
                         select o).Count();
            

            CurrentPage = pageIndex;
            if (pageIndex <= 0)
            {
                CurrentPage = 1;
            }
            int pageSize = 10;
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            id = 51;


            IQueryable<Comment> commentsIQ = from c in _context.Comments select c;
            Comments = await _context.Comments.FirstOrDefaultAsync(c => c.BlogID == id);
            CommentsList = await Pagination<Comment>.CreateAsync(
                commentsIQ.Where(c => c.BlogID == id).OrderByDescending(c => c.Date).AsNoTracking(), pageIndex ?? 1, pageSize);
            MaxPage = _context.Comments.Count(t => t.BlogID == id);
        }
    }
}
