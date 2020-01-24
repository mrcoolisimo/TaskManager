using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Tag
	{
		public int TagID { get; set; }
		public string Title { get; set; }
		public Blog Blog { get; set; }
	}
}