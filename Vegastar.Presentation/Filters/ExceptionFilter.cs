using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Vegastar.Domain.Exceptions;

namespace Vegastar.Presentation.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionFilter(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;
    
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            UserNotFoundException => new ContentResult
            {
                StatusCode = StatusCodes.Status404NotFound,
                Content = context.Exception.Message
            },
            AdminAlreadyExistsException or LoginTakenException => new ContentResult
            {
                StatusCode = StatusCodes.Status409Conflict,
                Content = context.Exception.Message
            },
            InvalidOperationException {InnerException: DbUpdateException {InnerException: PostgresException {Code: "40001"}}} => new ContentResult
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                Content = "User with this login is already being created."
            },
            _ => new ContentResult
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Content = _hostEnvironment.IsDevelopment()
                    ? context.Exception.Message
                    : "An inner on the server side occured"
            }
        };
    }
}