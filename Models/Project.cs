using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Project
	{
		public int ProjectID { get; set; }
		public string OwnerID { get; set; }
		public string Owner { get; set; }
		public string Title { get; set; }

		public ICollection<Tasking> Tasks { get; set; }
	}
}
