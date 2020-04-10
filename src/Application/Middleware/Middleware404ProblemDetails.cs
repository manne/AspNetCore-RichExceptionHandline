using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;

namespace Application.Middleware
{
    public class Middleware404ProblemDetails
    {
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IActionResultExecutor<ObjectResult> _executor;

        public Middleware404ProblemDetails(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory, IActionResultExecutor<ObjectResult> executor)
        {
            _next = next;
            _problemDetailsFactory = problemDetailsFactory;
            _executor = executor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            var responseStatusCode = context.Response.StatusCode;
            if (responseStatusCode == StatusCodes.Status404NotFound && !context.Response.HasStarted)
            {
                var pd = _problemDetailsFactory.CreateProblemDetails(context, StatusCodes.Status404NotFound, instance: context.Request.Path);
                var routeData = context.GetRouteData();
                var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
                await _executor.ExecuteAsync(actionContext, new ObjectResult(pd));
            }
        }
    }
}
