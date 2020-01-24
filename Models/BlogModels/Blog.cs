using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Blog
	{
		public int BlogID { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public DateTime Date { get; set; }
		public string Post { get; set; }
		public string Tags { get; set; }
		public int Likes { get; set; }
	}
}
