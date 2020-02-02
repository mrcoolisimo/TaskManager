using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
	public class Project
	{
		public int ProjectID { get; set; }
		public string OwnerID { get; set; }
		public string Owner { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Title { get; set; }

		public ICollection<Tasking> Tasks { get; set; }
		public ICollection<Member> Members { get; set; }
	}
}
