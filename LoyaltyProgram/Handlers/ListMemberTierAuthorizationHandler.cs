using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace LoyaltyProgram.Handlers
{
    public class ListMemberTierAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, List<MemberTier>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, List<MemberTier> resource)
        {
            var permissions = new List<Role>();
            var id = int.Parse(context.User.Claims.First(c => c.Type == ("id")).Value);
            if (context.User.HasClaim("role", "Admin") && requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }


            bool result = true;
            foreach (MemberTier tier in resource)
            {
                if (tier.LoyaltyMemberId != id)
                {
                    result = false;
                    break;
                }
            }


            if (context.User.HasClaim("role", "User") && result == true && requirement.Name == Operations.Read.Name)
            {

                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            return Task.CompletedTask;
        }
    }
}
