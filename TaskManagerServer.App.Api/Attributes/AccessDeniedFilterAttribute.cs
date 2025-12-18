using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagerServer.Lib.Domain.Exceptions;

namespace TaskManagerServer.App.Api.Attributes;

public class AccessDeniedFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ForbidResult)
        {
            context.Result = new ObjectResult(new ProblemDetails())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
        
        if (context.Exception is AccessDeniedException ex)
        {
            context.Result = new ObjectResult(new ProblemDetails())
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            //Let the system know that the exception has been handled
            context.ExceptionHandled = true;
        }
    }
}