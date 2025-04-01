using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException dbEx) when (dbEx.InnerException?.Message.Contains("IX_Products_Sku") == true)
        {
            _logger.LogWarning("El código SKU ya existe");

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = "DuplicateSku",
                message = "El código SKU ya existe. Intenta con uno diferente."
            });
        }
        catch (DbUpdateException dbEx)
        {
            _logger.LogError(dbEx, "Error no controlado de base de datos");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = "DatabaseError",
                message = "Ocurrió un error al procesar la base de datos"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = "UnexpectedError",
                message = "Error interno del servidor"
            });
        }
    }
}
