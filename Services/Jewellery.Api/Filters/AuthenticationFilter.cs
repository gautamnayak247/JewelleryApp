namespace Jewellery.Api.Filters
{
    using Jewellery.Domain.Entities;
    using Jewellery.Domain.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Threading.Tasks;
    using static Jewellery.Domain.Constant;

    /// <summary>
    /// Defines Authentication Filter
    /// </summary>
    public sealed class AuthenticationAttribute : TypeFilterAttribute
    {
        public AuthenticationAttribute() : base(typeof(AuthenticationFilter)) { }

    }
    /// <summary>
    /// Authentication Filter
    /// </summary>
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers[TokenHeader];

            var IsValid = IsValidToken(token, context);
            if (IsValid)
            {
                AuthorizationResult.Success();
                await SetUserContext(context, token);
            }
            else
            {
                AuthorizationResult.Failed();
                throw new UnAuthorizedException("User is not authorized");
            }
        }

        private bool IsValidToken(string token, AuthorizationFilterContext context)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }
            var isGuid = Guid.TryParse(token, out Guid result);
            if (isGuid)
            {
                var loginManager = context.HttpContext.RequestServices.GetService(typeof(ILogInManager)) as ILogInManager;
                var userId = loginManager.GetUserIdByToken(token);
                if (!userId.HasValue)
                {
                    return false;
                }
                var userContext = context.HttpContext.RequestServices.GetService(typeof(IUser)) as IUser;
                userContext.Id = userId.Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task SetUserContext(AuthorizationFilterContext context, string token)
        {
            var user = default(IUser);
            var userManager = context.HttpContext.RequestServices.GetService(typeof(IUserManager)) as IUserManager;
            var userContext = context.HttpContext.RequestServices.GetService(typeof(IUser)) as IUser;

            user = await userManager.GetUserByIdAsync(userContext.Id).ConfigureAwait(false);
            userContext.FirstName = user.FirstName;
            userContext.LastName = user.LastName;
            userContext.Type = user.Type;
            userContext.UserId = user.UserId;
            userContext.Id = user.Id;
        }
    }
}
