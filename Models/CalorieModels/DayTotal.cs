using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManger.Models
{
	public class DayTotal
	{
		public int DayTotalID { get; set; }
		public string Owner { get; set; }
		public int TotalFats { get; set; }
		public int TotalCarbs { get; set; }
		public int TotalProtein { get; set; }
		public string Date { get; set; }
		public DateTime RealDate { get; set; }
	}
}
