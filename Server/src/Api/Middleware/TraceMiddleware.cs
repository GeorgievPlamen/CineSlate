namespace Api.Middleware;

public class TraceMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Cookies.Append("TraceId", context.TraceIdentifier);

        await _next(context);
    }
}