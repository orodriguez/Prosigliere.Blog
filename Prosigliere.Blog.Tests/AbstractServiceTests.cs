using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;
using Prosigliere.Blog.Tests.Fakes;

namespace Prosigliere.Blog.Tests;

public abstract class AbstractServiceTests
{
    protected DateTime CurrentTime = DateTime.Now;
    private readonly ServiceProvider _provider;

    protected AbstractServiceTests() =>
        _provider = new ServiceCollection()
            .AddProsigliereBlog()
            .AddProsigliereBlogFakeRepositories()
            .AddTransient<Func<DateTime>>(_ => () => CurrentTime)
            .BuildServiceProvider();

    protected Result<PostResponse> CreatePost(CreatePostRequest request) => 
        _provider.GetRequiredService<IPostsService>().Create(request);

    protected Result<PostResponse> GetPostById(int id) => 
        _provider.GetRequiredService<IPostsService>().ById(id);

    protected Result<CreateCommentResponse> CreateComment(CreateCommentRequest request) => 
        _provider.GetRequiredService<ICommentsService>().Create(request);
}