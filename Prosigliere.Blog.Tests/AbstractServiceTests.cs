using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;

namespace Prosigliere.Blog.Tests;

public abstract class AbstractServiceTests
{
    protected readonly IPostsService Service;
    protected DateTime CurrentTime = DateTime.Now;
    protected readonly FakeRepository<Post> FakePostsRepository;

    protected AbstractServiceTests()
    {
        FakePostsRepository = new FakeRepository<Post>();
        
        Service = new PostsService(
            new FluentValidatorAdapter<CreatePostRequest>(new CreatePostRequestValidator()),
            blogPostsRepository: FakePostsRepository,
            currentTime: () => CurrentTime);
    }

    protected (PostResponse?, Errors?) CreatePost(CreatePostRequest request) => 
        Service.Create(request);
}