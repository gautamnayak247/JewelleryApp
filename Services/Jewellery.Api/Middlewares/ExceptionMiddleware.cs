namespace Jewellery.Api.Middlewares
{
    using Jewellery.Domain.Entities;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using static Jewellery.Domain.Constant;
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate _next)
        {
            next = _next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var message = default(string);

            if (exception is UnAuthorizedException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exception is DBException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = "Error in persistence";
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = GlobalErrorMessage;
            }
            var response = new Error();
            response.StatusCode = context.Response.StatusCode;
            response.Message = message;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
