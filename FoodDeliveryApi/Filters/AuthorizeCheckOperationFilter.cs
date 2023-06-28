using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoodDeliveryApi.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

        // Check if the action method has the [AllowAnonymous] attribute
        var hasAllowAnonymousAttribute = descriptor?.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>()
            .Any() ?? false;

        // Check if the action method has the [Authorize] attribute
        var hasAuthorizeAttribute = descriptor?.MethodInfo.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Any() ?? false;

        if (!hasAllowAnonymousAttribute && hasAuthorizeAttribute)
        {
            // Apply authorization filters to hide unauthorized endpoints
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    // Add the authentication scheme that corresponds to the cookie authentication
                    // Example:
                    // [CookieAuth] or [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
                    [new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "CookieAuth" } }] = new List<string>()
                }
            };
        }
    }
}