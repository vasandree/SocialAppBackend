using System.Text;

public class JsonBodyMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<JsonBodyMiddleware> _logger;

    public JsonBodyMiddleware(RequestDelegate next, ILogger<JsonBodyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post &&
            context.Request.ContentType == "application/json" &&
            context.Request.Body.CanSeek)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            _logger.LogInformation("[Gateway] Request body: {Body}", body);
            context.Request.Body.Position = 0;
        }

        await _next(context);
    }
}