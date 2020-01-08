using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Authorization
{
	public class OwnerAHTasking : AuthorizationHandler<OperationAuthorizationRequirement, Tasking>
	{
		UserManager<IdentityUser> _userManager;

		public OwnerAHTasking(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		protected override Task
			HandleRequirementAsync(AuthorizationHandlerContext context,
								OperationAuthorizationRequirement requirement,
								Tasking resource)
		{
			//If no user or resource
			if (context.User == null || resource == null)
			{
				//Discontinue this script
				//return Task.CompletedTask;
				return Task.CompletedTask;
			}
			//If the user matches the OwnerID on this object, then the CRUD request succeeds
			if (resource.Project.OwnerID == _userManager.GetUserId(context.User))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
