using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Middleware
{
    public class Middleware404ProblemDetails
    {
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        private readonly RequestDelegate _next;

        public Middleware404ProblemDetails(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var responseStatusCode = context.Response.StatusCode;
            if (responseStatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                var factory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                var pd = factory.CreateProblemDetails(context, StatusCodes.Status404NotFound, instance: context.Request.Path);
                var executor = context.RequestServices.GetService<IActionResultExecutor<ObjectResult>>();
                var routeData = context.GetRouteData();

                var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
                await executor.ExecuteAsync(actionContext, new ObjectResult(pd));
            }
        }
    }
}
