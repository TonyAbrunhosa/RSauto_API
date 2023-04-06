using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RSauto.Domain.Entities.Command;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.API.Middlewares
{
    public class BaseMiddleware
    {
        private readonly RequestDelegate _next;

        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new CommandResult(false, ex.Message + ex.StackTrace)));
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.Body.WriteAsync(data);                
            }
        }
    }

    public static class BaseMiddlewareExtensions
    {
        public static IApplicationBuilder UseBaseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BaseMiddleware>();
        }
    }
}
