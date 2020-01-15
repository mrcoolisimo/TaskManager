using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Member
	{
		public int MemberID { get; set; }
		public int ProjectID { get; set; }
		public string ProjectName { get; set; }
		public string Email { get; set; }
		public int IsOwner { get; set; }

		public Project Project { get; set; }
	}
}
