using Prosigliere.Blog.Api;
using Xunit;

namespace Prosigliere.Blog.Tests;

public static class ResultExtensions
{
    public static TValue AssertSuccess<TValue>(this Result<TValue> result) => 
        Assert.IsType<Result<TValue>.Success>(result).Value;
    
    public static Errors AssertValidationErrors<TValue>(this Result<TValue> result) => 
        Assert.IsType<Result<TValue>.ValidationErrors>(result).Errors;
}