namespace Jewellery.Api.Core
{
    using Jewellery.Api.Middlewares;
    using Microsoft.AspNetCore.Builder;

    public static class MiddlewareConfiguration
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
              => app.UseMiddleware<ExceptionMiddleware>();
        public static void UseNotAcceptableMiddleware(this IApplicationBuilder app)
            => app.UseMiddleware<ContentNegotiationMiddleware>();
    }
}
