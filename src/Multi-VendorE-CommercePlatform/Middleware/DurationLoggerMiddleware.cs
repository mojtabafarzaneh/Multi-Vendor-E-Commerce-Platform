using System.Diagnostics;

namespace Multi_VendorE_CommercePlatform.Middleware;

public class DurationLoggerMiddleware
{
    private readonly ILogger<DurationLoggerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public DurationLoggerMiddleware(RequestDelegate next, ILogger<DurationLoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var requestUrl = context.Request.Path;
        try
        {
            await _next(context);
        }
        finally
        {
            var text = $"[{requestUrl}]: {sw.ElapsedMilliseconds} ms";
            _logger.LogInformation(text);
        }
    }
}