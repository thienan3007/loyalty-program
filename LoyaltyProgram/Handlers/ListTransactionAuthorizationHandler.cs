﻿using LoyaltyProgram.Auth;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace LoyaltyProgram.Handlers
{
    public class ListTransactionAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, List<Transaction>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, List<Transaction> resource)
        {
            var permissions = new List<Role>();
            var id = int.Parse(context.User.Claims.First(c => c.Type == ("id")).Value);
            if (context.User.HasClaim("role", "Admin") && requirement.Name == Operations.Read.Name)
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            bool result = true;
            foreach (Transaction transaction in resource)
            {
                if (transaction.MembershipId != id)
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
