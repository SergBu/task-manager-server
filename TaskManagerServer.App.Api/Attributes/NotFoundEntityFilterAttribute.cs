using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskManagerServer.Lib.Domain.Exceptions;

namespace TaskManagerServer.App.Api.Attributes;

public class NotFoundEntityFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is NotFoundEntityException ex)
        {
            context.Result = new ObjectResult(ex.Message)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            //Let the system know that the exception has been handled
            context.ExceptionHandled = true;
        }
    }
}