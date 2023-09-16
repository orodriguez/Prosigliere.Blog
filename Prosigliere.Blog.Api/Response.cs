using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.Api;

public abstract record Response<TValue>
{
    public record ValidationErrors(Errors Errors) : Response<TValue>;

    public record Success(TValue Value) : Response<TValue>;
}