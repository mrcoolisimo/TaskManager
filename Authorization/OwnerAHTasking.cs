using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
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

			/*
			if (requirement.Name == Constants.UpdateOperationName)
			{
				if	(resource.TaskOwner == _userManager.GetUserId(context.User))
				{
					context.Succeed(requirement);
				}
				else
				{
					return Task.CompletedTask;
				}
			}
			*/

			//If the user matches the OwnerID on this object, then the CRUD request succeeds
			//If you created your task, you have full ownership of the task
			if (resource.Project.OwnerID == _userManager.GetUserId(context.User) ||
				resource.TaskOwner == _userManager.GetUserId(context.User) ||
				resource.Assignment == _userManager.GetUserId(context.User))
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
