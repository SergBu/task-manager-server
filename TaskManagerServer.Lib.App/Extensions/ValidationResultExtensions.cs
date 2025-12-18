using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagerServer.Lib.App.Extensions;

/// <summary>
/// ValidationResultExtensions
/// </summary>
public static class ValidationResultExtensions
{
    /// <summary>
    /// ConvertToModelState
    /// </summary>
    /// <param name="result"></param>
    public static ModelStateDictionary ConvertToModelState(this ValidationResult? result)
    {
        var modelState = new ModelStateDictionary();
        if(result == null)
            return modelState;
        if (result.IsValid)
            return modelState;
        
        foreach (var error in result.Errors) {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return modelState;
    }
}