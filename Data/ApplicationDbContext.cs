using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<TaskManager.Models.Project> Project { get; set; }
		public DbSet<TaskManager.Models.Tasking> Tasking { get; set; }
		public DbSet<TaskManager.Models.Member> Member { get; set; }
		public DbSet<TaskManager.Models.Blog> Blog { get; set; }
		public DbSet<TaskManager.Models.UserLike> UserLike { get; set; }
		public DbSet<TaskManager.Models.Comment> Comments { get; set; }
	}
}
