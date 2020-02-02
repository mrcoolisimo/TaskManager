using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Comment
	{
		public int CommentID { get; set; }
		public int BlogID { get; set; }
		public string Author { get; set; }
		public DateTime Date { get; set; }
		[Column(TypeName = "nvarchar(1000)")]
		public string Post { get; set; }

		public Blog Blog { get; set; }
	}
}
