using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Fakes;
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

    protected Result<DetailedPostResponse> CreatePost(CreatePostRequest request) => 
        CreatePostsService().Create(request);

    protected Result<DetailedPostResponse> GetPostById(int id) => 
        CreatePostsService().ById(id);

    protected Result<CommentResponse> CreateComment(int postId, CreateCommentRequest request) => 
        _provider.GetRequiredService<ICommentsService>().Create(postId, request);

    protected Result<IEnumerable<ShortPostResponse>> GetPosts() => CreatePostsService().Get();

    private IPostsService CreatePostsService() => 
        _provider.GetRequiredService<IPostsService>();
}