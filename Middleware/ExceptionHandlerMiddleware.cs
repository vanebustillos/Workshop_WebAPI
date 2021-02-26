using System;
using System.Net;
using System.Threading.Tasks;

using BusinessLogic.Exceptions;
using Database.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Middleware
{// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project, uh oh
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                Console.WriteLine("This is the Exception Middleware");
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleError(httpContext, ex);
            }
        }

        private static Task HandleError(HttpContext httpContext, Exception ex)
        {
            int httpStatusCode;
            string messageToShow;

            if (ex is EmptyDatabaseException) //The database is empty
            {
                httpStatusCode = 400;
                messageToShow = ex.Message;
            }
            else if (ex is InvalidWorkshopNameException) //The input name is empty or null
            {
                httpStatusCode = 404;
                messageToShow = ex.Message;
            }
            else if (ex is InvalidWorkshopStatusException) //The input status is empty or null
            {
                httpStatusCode = 404;
                messageToShow = ex.Message;
            }
            else if (ex is WorkshopNotFoundException) //There isn't any workshop matched.
            {
                httpStatusCode = 400;
                messageToShow = ex.Message;
            }
            else if (ex is InvalidOperationException) //The requested object is in an inapropiate state
            {
                httpStatusCode = (int)HttpStatusCode.NotAcceptable;
                messageToShow = ex.Message;
            }
            else if (ex is NotImplementedException) //There is no implemented method
            {
                httpStatusCode = (int)HttpStatusCode.NotImplemented;
                messageToShow = ex.Message;
            }
            else
            {
                httpStatusCode = (int)HttpStatusCode.InternalServerError;
                messageToShow = "The server occurs an unexpected error.";
            }

            var errorModel = new
            {
                status = httpStatusCode,
                message = messageToShow
            };

            // httpContext.Response.StatusCode = httpStatusCode;
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorModel));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}