using API.Errors;
using System.Net;
using System.Text.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace API.Middleware
{
    public class ExceptionMiddleware(IHostingEnvironment env,RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandelExceptionAsync(context,ex,env);
            }
        }

        private static Task HandelExceptionAsync(HttpContext context, Exception ex, IHostingEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            var response = env.IsDevelopment()
                ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace) :
                new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal server error");
            var options=new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
            var json =JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(json);
        }
    }
}
