using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;

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

    protected (PostResponse?, Errors?) CreatePost(CreatePostRequest request) => 
        _provider.GetRequiredService<IPostsService>().Create(request);

    protected PostResponse GetPostById(int id) => 
        _provider.GetRequiredService<IPostsService>().ById(id);

    protected void CreateComment(CreateCommentRequest request) => 
        _provider.GetRequiredService<ICommentsService>().Create(request);
}