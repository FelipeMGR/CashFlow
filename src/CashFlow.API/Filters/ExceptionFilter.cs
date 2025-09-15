using CashFlow.Communication.Response;
using CashFlow.Exceptions;
using CashFlow.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is CashFlowException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private static void ThrowUnknowError(ExceptionContext context)
        {
            ResponseErrorJson errorMessage = new(ResourcesErrorMessages.UNKNOW_ERROR);

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorMessage);
        }

        private static void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                ErrorOnValidationException? ex = context.Exception as ErrorOnValidationException;

                ResponseErrorJson errorMessage = new(ex.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorMessage);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }
    }
}
