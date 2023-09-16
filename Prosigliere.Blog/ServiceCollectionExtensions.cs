using Microsoft.Extensions.DependencyInjection;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Posts;
using Prosigliere.Blog.Validations;

namespace Prosigliere.Blog;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProsigliereBlog(this IServiceCollection services)
    {
        services.AddTransient<IPostsService, PostsService>();
        services.AddTransient<IValidator<CreatePostRequest>, FluentValidatorAdapter<CreatePostRequest>>();
        services.AddTransient<IValidator<CreateCommentRequest>, FluentValidatorAdapter<CreateCommentRequest>>();
        services.AddTransient<FluentValidation.IValidator<CreatePostRequest>, CreatePostRequestValidator>();
        services.AddTransient<FluentValidation.IValidator<CreateCommentRequest>, CreateCommentRequestValidator>();
        services.AddTransient<ICommentsService, CommentsService>();
        services.AddTransient<Func<DateTime>>(_ => () => DateTime.Now);
        return services;
    }
}