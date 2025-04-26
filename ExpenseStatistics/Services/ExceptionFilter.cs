using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;
using ExpenseStatistics.Exceptions;

namespace ExpenseStatistics.Services
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var errors = context.Exception is ValidationException vex
                ? vex.Errors.Select(e => e.ErrorMessage)
                : null;

            context.Result = new ObjectResult(new ErrorResponse
            {
                Message = context.Exception.Message,
                Errors = errors
            })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}