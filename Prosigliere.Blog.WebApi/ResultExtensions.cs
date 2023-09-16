using Microsoft.AspNetCore.Mvc;
using Prosigliere.Blog.Api;

namespace Prosigliere.Blog.WebApi;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> response) => response switch
    {
        Result<T>.Success success => new OkObjectResult(success.Value),
        Result<T>.ValidationErrors validationErrors => new BadRequestObjectResult(validationErrors.Errors), 
        Result<T>.RecordNotFound recordNotFound => new NotFoundObjectResult(recordNotFound.Message),
        _ => throw new ArgumentOutOfRangeException(nameof(response))
    };
}