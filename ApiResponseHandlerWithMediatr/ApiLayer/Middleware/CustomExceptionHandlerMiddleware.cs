using ApiResponseHandlerWithMediatr.ApiLayer.ResponseHandlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ApiResponseHandlerWithMediatr.ApiLayer.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiResponseWriter _responseWriter;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ApiResponseWriter responseWriter)
        {
            _next = next;
            _responseWriter = responseWriter;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                await HandleApiResponseAsync(context, _responseWriter);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        //This method will replace the handled custom exceptions using Mediatr notification handlers. 
        private async Task HandleApiResponseAsync(HttpContext context, ApiResponseWriter responseWriter)
        {
            if (responseWriter.IsSet)
            {
                var (statusCode, message) = responseWriter.Get();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                responseWriter.Reset();

                await context.Response.WriteAsync(message);
            }
        }

        //This method will be called for unexpected/unhandled exceptions
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
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
