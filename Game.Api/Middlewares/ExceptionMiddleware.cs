using System.Net;
using System.Text.Json;
using Game.Application.Dtos;
using Game.Application.Exceptions;

namespace Game.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            ErrorResponse response = new();

            if (exception is AppException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                response.Error = exception.Message;
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Error = "Internal Server Error";
            }

            logger.LogError(exception, exception.Message);
            httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}