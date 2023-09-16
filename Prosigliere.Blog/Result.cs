using Prosigliere.Blog.Api;

namespace Prosigliere.Blog;

public static class Result
{
    public static Result<T> Success<T>(T value) => 
        new Result<T>.Success(value);
    
    public static Result<T> ValidationErrors<T>(Errors errors) => 
        new Result<T>.ValidationErrors(errors);
    
    public static Result<T> RecordNotFound<T>(string message) => 
        new Result<T>.RecordNotFound(message);
}