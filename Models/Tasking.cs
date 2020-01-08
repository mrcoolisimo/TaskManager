using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class Tasking
	{
		public int TaskingID { get; set; }
		public int ProjectID { get; set; }
		public string Description { get; set; }
		public Severity? Severity { get; set; }
		public Progression? Progression { get; set; }

		public Project Project { get; set; }
	}

	public enum Severity
	{
		Low,
		Medium,
		High
	}
	public enum Progression
	{
		Initial,
		Started,
		Complete
	}
}

