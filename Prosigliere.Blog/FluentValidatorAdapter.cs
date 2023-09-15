using FluentValidation.Results;
using Prosigliere.Blog.Api;

namespace Prosigliere.Blog;

/// <summary>
/// Adapts FluentValidator adapters to Prosigliere.Blog.IValidator<T> interface.
/// Decouples the application from an specific validator library, in this case FluentValidator
/// </summary>
/// <typeparam name="T">Type of the object to validate</typeparam>
public class FluentValidatorAdapter<T> : IValidator<T>
{
    private readonly FluentValidation.IValidator<T> _fluentValidator;

    public FluentValidatorAdapter(FluentValidation.IValidator<T> fluentValidator) => 
        _fluentValidator = fluentValidator;

    public Errors Validate(T obj)
    {
        var failures = _fluentValidator
            .Validate(obj)
            .Errors;

        var errors = failures
            .GroupBy(failure => failure.PropertyName)
            .ToDictionary(
                grouping => grouping.Key,
                grouping => grouping.Select(failure => failure.ErrorMessage).ToArray());
        
        return new Errors(errors);
    }
}