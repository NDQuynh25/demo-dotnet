

using System;

namespace netcore_server.DTOs.Response;
public class ApiResponse<T>
{
    public ApiResponse()
    {
    }

    public ApiResponse(bool? success, int? statusCode, string? error, string? message, T? data)
    {
        Success = success ?? true;
        StatusCode = statusCode ?? 200;
        Error = error ?? string.Empty;
        Message = message ?? string.Empty;
        Data = data ?? default!;
        Timestamp = DateTime.Now;
    }
    
    public bool Success { get; set; } = true;

    public int StatusCode { get; set; } = 200;

    public string Error { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public T Data { get; set; } = default!;

    public DateTime Timestamp { get; set; } = DateTime.Now;
}