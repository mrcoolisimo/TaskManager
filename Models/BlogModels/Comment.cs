using System;
using System.Collections.Generic;
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
		public string Post { get; set; }

		public Blog Blog { get; set; }
	}
}
