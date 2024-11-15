using System.Diagnostics;

namespace Api.Middleware;

public class TraceMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Cookies.Append("TraceId", Activity.Current?.Id ?? "Failed to get");

        await _next(context);
    }
}