using FUForum.BackendServer.Middlewares;

namespace FUForum.BackendServer.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorWrapping(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorWrappingMiddleware>();
    }
}