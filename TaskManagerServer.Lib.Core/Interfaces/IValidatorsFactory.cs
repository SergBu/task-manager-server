using FluentValidation;

namespace TaskManagerServer.Lib.Core.Interfaces;

/// <summary>
/// Factory
/// </summary>
public interface IValidatorsFactory
{
    /// <summary>
    /// Get validator
    /// </summary>
    /// <returns></returns>
    IValidator<T> GetValidator<T>();

    bool TryGetValidator(object model, out IValidator validator);
}