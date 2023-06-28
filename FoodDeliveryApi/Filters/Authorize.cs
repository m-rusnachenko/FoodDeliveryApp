using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodDeliveryApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace FoodDeliveryApi.Filters;

public class RoleRequirement : IAuthorizationRequirement
{
    public Role RequiredRole { get; }

    public RoleRequirement(Role requiredRole)
    {
        RequiredRole = requiredRole;
    }

    public RoleRequirement(string requiredRole)
    {
        RequiredRole = (Role)Enum.Parse(typeof(Role), requiredRole);
    }
}

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        if (!context.User.Identity!.IsAuthenticated)
        {
            return Task.CompletedTask;
        }

        // Get the user's role claim value parse it to the role enum and represent it as an integer
        var userRoleClaimValue = (int)Enum
            .Parse(typeof(Role), 
                    context.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty);

        // Get the required role as an integer
        var requiredRoleValue = (int)requirement.RequiredRole;

        // Check if the user's role is bigger or equal to the required role
        if (userRoleClaimValue >= requiredRoleValue)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}