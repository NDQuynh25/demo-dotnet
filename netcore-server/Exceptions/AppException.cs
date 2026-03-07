namespace netcore_server.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; }

    public string Error { get; }

    public AppException(string message, int statusCode = 400, string error = "BadRequest")
        : base(message)
    {
        StatusCode = statusCode;
        Error = error;
    }
}