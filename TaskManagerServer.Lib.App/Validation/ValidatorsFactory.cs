using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerServer.Lib.Core.Interfaces;

namespace TaskManagerServer.Lib.App.Validation;

public class ValidatorsFactory(IServiceProvider serviceProvider) : IValidatorsFactory
{
    public IValidator<T> GetValidator<T>()
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));
        var validator = (IValidator<T>)serviceProvider.GetRequiredService(validatorType);
        return validator;
    }

    public bool TryGetValidator(object model, out IValidator validator)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(model.GetType());
        var service = serviceProvider.GetService(validatorType);
        if (service != null)
        {
            validator = (IValidator)service;
            return true;
        }

        validator = default!;
        return false;
    }
}