using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Authorization
{
	public class OwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Project>
	{
		UserManager<IdentityUser> _userManager;

		public OwnerAuthorizationHandler(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		protected override Task
			HandleRequirementAsync(AuthorizationHandlerContext context,
								OperationAuthorizationRequirement requirement,
								Project resource)
		{



			//If no user or resource
			if (context.User == null || resource == null)
			{
				//Discontinue this script
				//return Task.CompletedTask;
				return Task.CompletedTask;
			}
			//If the user matches the OwnerID on this object, then the CRUD request succeeds
			if (resource.OwnerID == _userManager.GetUserId(context.User))
			{
				context.Succeed(requirement);
			}

			/*
			if(resource.Members == null)
			{
				return Task.CompletedTask;
			}
			*/
			if (resource.Members != null)
			{
				if (resource.Members.Any(m => m.Email == _userManager.GetUserName(context.User)))
				{
					context.Succeed(requirement);
				}
			}

			return Task.CompletedTask;
		}
	}
}
