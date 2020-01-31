using Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private string ReturnBadRequestResponse(RestException error)
        {
            var errorResponse = JsonConvert.SerializeObject(new
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Validation errors encountered. Please see below error messages.",
                ErrorMessage = error.Errors,
                ErrorDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            });

            Log.Error(errorResponse, "[REST ERROR]");

            return errorResponse;
        }

        private string ReturnInternalServerResponse(Exception error)
        {
            var errorResponse = JsonConvert.SerializeObject(new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "The app has encountered an Internal Server Error. Please try again.",
                ErrorDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            });

            return errorResponse;
        }

        private string ReturnNotFoundResponse(Exception error)
        {
            var errorResponse = JsonConvert.SerializeObject(new
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = error.Message,
                ErrorDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
            });

            return errorResponse;
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case RestException re:
                    context.Response.StatusCode = (int)re.Code;
                    await context.Response.WriteAsync(ReturnBadRequestResponse(re));
                    break;
                case NotFoundException re:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync(ReturnNotFoundResponse(re));
                    break;
                case Exception e:
                    Log.Error(e, "[SERVER ERROR]");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(ReturnInternalServerResponse(e));
                    break;
            }

            
         
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
