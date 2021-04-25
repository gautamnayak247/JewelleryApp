namespace Jewellery.Api.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using static Jewellery.Domain.Constant;

    public class ContentNegotiationMiddleware
    {
        private readonly RequestDelegate next;
        private List<string> acceptedHeaders;
        private readonly string headers;
        public ContentNegotiationMiddleware(RequestDelegate _next, IConfiguration configuration)
        {
            next = _next;
            headers = configuration[AllowedHeaders];
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (headers.Contains(','))
                acceptedHeaders = headers.Split(',').ToList<string>();
            else
                acceptedHeaders = new List<string> { headers };
            var acceptHeader = httpContext.Request.Headers.ContainsKey("Accept") ?
                            httpContext.Request.Headers["Accept"].ToString() :
                            string.Empty;
            if (acceptedHeaders.Contains(acceptHeader))
            {
                await next(httpContext);
            }
            else
            {
                await HandleNotAcceptableAsync(httpContext).ConfigureAwait(false);
            }
        }

        private async Task HandleNotAcceptableAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
            }.ToString()).ConfigureAwait(false);
        }

    }
}
