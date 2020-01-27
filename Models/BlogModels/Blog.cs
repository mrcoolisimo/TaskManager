﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models.BlogModels;

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

		public ICollection<UserLike> TrackLikes { get; set; }
		public ICollection<Comment> Comments { get; set; }
	}
}
