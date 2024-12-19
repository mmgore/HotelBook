using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HotelBook.Application.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await LogRequest(context);

            var originalResponseBodyStream = context.Response.Body;
            
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);
                await LogResponse(context);
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while processing the request: {ex.Message}");
            //await HandleExceptionAsync(context, ex);
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();
        var request = context.Request;

        var requestBodyStream = new StreamReader(request.Body, Encoding.UTF8);
        var body = await requestBodyStream.ReadToEndAsync();

        _logger.LogInformation($"[Request] {request.Method} {request.Path} - Body: {body}");

        request.Body.Position = 0;
    }

    private async Task LogResponse(HttpContext context)
    {
        var response = context.Response;

        response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(response.Body).ReadToEndAsync();

        _logger.LogInformation($"[Response] {response.StatusCode} - Body: {responseBody}");

        response.Body.Seek(0, SeekOrigin.Begin);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            Error = "Internal Server Error",
            Details = exception.Message
        };

        var errorJson = System.Text.Json.JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(errorJson);
    }
}