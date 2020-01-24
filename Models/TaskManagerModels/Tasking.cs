namespace TaskManager.Models
{
	public class Tasking
	{
		public int TaskingID { get; set; }
		public int ProjectID { get; set; }
		public string TaskOwner { get; set; }
		public string TaskOwnerName { get; set; }

		public string TaskTitle { get; set; }
		public string Description { get; set; }
		public int Severity { get; set; }
		public int Progression { get; set; }
		public string Assignment { get; set; }

		public Project Project { get; set; }
		public Member Member { get; set; }
	}
}

