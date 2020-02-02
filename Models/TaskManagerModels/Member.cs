using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
	public class Member
	{
		public int MemberID { get; set; }
		public int ProjectID { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string ProjectName { get; set; }
		[Column(TypeName = "nvarchar(300)")]
		public string Email { get; set; }
		public int IsOwner { get; set; }

		public Project Project { get; set; }
	}
}
