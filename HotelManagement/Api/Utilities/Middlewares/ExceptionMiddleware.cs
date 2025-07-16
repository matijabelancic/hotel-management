using System.Text.Json;
using Api.Utilities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using InvalidOperationException = Api.Utilities.Exceptions.InvalidOperationException;

namespace Api.Utilities.Middlewares;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        InvalidOperationException => StatusCodes.Status422UnprocessableEntity,
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
                    {
                        logger.LogCritical(contextFeature.Error, contextFeature.Error.Message);
                    }
                    else
                    {
                        logger.LogError(contextFeature.Error.Message);
                    }

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        context.Response.StatusCode,
                        contextFeature.Error.Message
                    }));
                }
            });
        });
    }
}