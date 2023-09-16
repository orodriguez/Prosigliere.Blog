using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.Api;

public abstract record Result<TValue>
{
    public record Success(TValue Value) : Result<TValue>;

    public record ValidationErrors(Errors Errors) : Result<TValue>;
}