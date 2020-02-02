using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Blog
	{
		public int BlogID { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Title { get; set; }
		public string Author { get; set; }
		public DateTime Date { get; set; }
		[Column(TypeName = "nvarchar(4000)")]
		public string Post { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Tags { get; set; }
		public int Likes { get; set; }

		public ICollection<UserLike> TrackLikes { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}
