using Prosigliere.Blog.Api;

namespace Prosigliere.Blog;

public interface IValidator<in T>
{
    Errors Validate(T obj);
}