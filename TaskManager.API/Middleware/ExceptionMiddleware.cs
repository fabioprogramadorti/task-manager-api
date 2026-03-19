using System.Net;
using System.Text.Json;

namespace TaskManager.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        
        if (ex.Message.Contains("não encontrado"))
            statusCode = HttpStatusCode.NotFound;

        if (ex.Message.Contains("já cadastrado"))
            statusCode = HttpStatusCode.BadRequest;

        var response = new
        {
            status = (int)statusCode,
            message = ex.Message,
            timestamp = DateTime.UtcNow
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}