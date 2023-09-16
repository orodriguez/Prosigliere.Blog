using Prosigliere.Blog.Api;

namespace Prosigliere.Blog.Validations;

public interface IValidator<in T>
{
    Errors Validate(T obj);
}