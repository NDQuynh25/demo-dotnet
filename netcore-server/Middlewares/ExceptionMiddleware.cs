using System.Text.Json;
using netcore_server.DTOs.Response;
using netcore_server.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await HandleException(context, ex.StatusCode, ex.Error, ex.Message);
        }
        catch (Exception)
        {
            await HandleException(context, 500, "InternalServerError", "Internal server error");
        }
    }

    private static async Task HandleException(
        HttpContext context,
        int statusCode,
        string error,
        string message
    )
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ApiResponse<object>(
            false,
            statusCode,
            error,
            message,
            null
        );

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}