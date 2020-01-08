using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Pages.Projects.Taskings
{
    public class IndexModel : PageModel
    {
        private readonly TaskManager.Data.ApplicationDbContext _context;

        public IndexModel(TaskManager.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Tasking> Tasking { get;set; }

        public async Task OnGetAsync()
        {
            Tasking = await _context.Tasking
                .Include(t => t.Project).ToListAsync();
        }
    }
}
