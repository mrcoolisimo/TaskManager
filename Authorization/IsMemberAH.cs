using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Authorization
{
	public class IsMemberAH : AuthorizationHandler<OperationAuthorizationRequirement, Member>
	{
		UserManager<IdentityUser> _userManager;

		public IsMemberAH(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;
		}

		protected override Task
			HandleRequirementAsync(AuthorizationHandlerContext context,
								OperationAuthorizationRequirement requirement,
								Member resource)
		{



			//If no user or resource
			if (context.User == null || resource == null)
			{
				//Discontinue this script
				//return Task.CompletedTask;
				return Task.CompletedTask;
			}
			//If the user matches the OwnerID on this object, then the CRUD request succeeds
			if (resource.Email == _userManager.GetUserId(context.User))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}