using Domain.Interface;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Application.Interface;
using Application.Helper;
using Infrastructure.Constant;

namespace WebApi.Extensions
{
    public class CustomAuthorizationExtension : AuthorizationHandler<ClaimsAuthorizationRequirement>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IPermissionService _permissionService;

        public CustomAuthorizationExtension(ICurrentUser currentUser, IPermissionService permissionService)
        {
            _currentUser = currentUser;
            _permissionService = permissionService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ClaimsAuthorizationRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var claims = await _permissionService.GetPermissionsByUserId(_currentUser.UserId);
            Utils.UserId = _currentUser.UserId;
            this._currentUser.SetClaims(claims.Select(claim => new System.Security.Claims.Claim(AppConstants.Permission, claim)));

            foreach (var item in requirement.AllowedValues)
            {
                if (!claims.Contains(item))
                {
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }
    }
}
