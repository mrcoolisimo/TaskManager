using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Authorization
{
	public class LoggedIn : AuthorizationHandler<OperationAuthorizationRequirement, Project>
	{
		UserManager<IdentityUser> _userManager;

		public LoggedIn(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		protected override Task
			HandleRequirementAsync(AuthorizationHandlerContext context,
								OperationAuthorizationRequirement requirement,
								Project resource)
		{
			if (context.User == null)
			{
				//Discontinue this script
				return Task.CompletedTask;
			}
			if (context.User != null)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
