using System.Collections;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManagerServer.Lib.App.Extensions;
using TaskManagerServer.Lib.Core.Interfaces;
using TaskManagerServer.Lib.Domain.Exceptions;

namespace TaskManagerServer.App.Api.Attributes;

public class ValidateModelFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
            });
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
            });
    }
    
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is UnprocessableEntityException unprocessableEntityException)
        {
            context.Result = new ObjectResult(unprocessableEntityException.Message)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };

            //Let the system know that the exception has been handled
            context.ExceptionHandled = true;
        }
    }
    
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
            });
        
        var factory = context.HttpContext.RequestServices.GetRequiredService<IValidatorsFactory>();
        var modelState = new ModelStateDictionary();
        
        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument == null || !argument.GetType().IsClass)
            {
                continue;
            }

            if (argument is IEnumerable argumentList)
            {
                foreach (var item in argumentList)
                {
                    await ValidateArgument(factory, item, modelState);
                }
            }
            else
            {
                await ValidateArgument(factory, argument, modelState);
            }

            if (!modelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(modelState);
            }
        }

        await next();
    }

    private static async Task ValidateArgument(IValidatorsFactory factory, object item, ModelStateDictionary modelState)
    {
        if (!factory.TryGetValidator(item, out var validator))
        {
            return;
        }
        
        var validationContextType = typeof(ValidationContext<>).MakeGenericType(item.GetType());
        var validationContext = (IValidationContext?)Activator.CreateInstance(validationContextType, item);

        if (validationContext != null)
        {
            var result = await validator.ValidateAsync(validationContext);
            if (!result.IsValid)
            {
                modelState.Merge(result.ConvertToModelState());
            }
        }
    }
}