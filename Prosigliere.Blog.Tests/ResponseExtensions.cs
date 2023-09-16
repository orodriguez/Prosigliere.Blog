using Prosigliere.Blog.Api;
using Xunit;

namespace Prosigliere.Blog.Tests;

public static class ResponseExtensions
{
    public static TValue AssertSuccess<TValue>(this Response<TValue> response) => 
        Assert.IsType<Response<TValue>.Success>(response).Value;
    
    public static Errors AssertValidationErrors<TValue>(this Response<TValue> response) => 
        Assert.IsType<Response<TValue>.ValidationErrors>(response).Errors;
}