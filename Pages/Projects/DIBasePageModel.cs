﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Pages.Projects
{
	public class DIBasePageModel : PageModel
	{
		//Pages will implement these services
		protected ApplicationDbContext Context { get; }
		//This acceses the authorization handlers
		protected IAuthorizationService AuthorizationService { get; }
		protected UserManager<IdentityUser> UserManager { get; }

		public DIBasePageModel(
			ApplicationDbContext context,
			IAuthorizationService authorizationService,
			UserManager<IdentityUser> userManager) : base()
		{
			Context = context;
			UserManager = userManager;
			AuthorizationService = authorizationService;
		}
	}
}
