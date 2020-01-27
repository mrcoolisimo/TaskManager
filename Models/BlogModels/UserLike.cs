using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
	public class UserLike
	{
		public int UserLikeID { get; set; }
		public int BlogID { get; set; }
		public string UserID { get; set; }
		public int IsLiked { get; set; }

		public Blog Blog { get; set; }
	}
}
