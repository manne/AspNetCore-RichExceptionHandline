using Application.Middleware;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseProblemDetailsFor404(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware404ProblemDetails>();
        }
    }
}
