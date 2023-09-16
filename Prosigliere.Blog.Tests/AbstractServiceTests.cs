using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;

namespace Prosigliere.Blog.Tests;

public abstract class AbstractServiceTests
{
    private readonly IPostsService _postsService;
    protected DateTime CurrentTime = DateTime.Now;
    private ICommentsService _commentsService;

    protected AbstractServiceTests()
    {
        FakeRepository<Post> fakePostsRepository = new();
        
        _postsService = new PostsService(
            new FluentValidatorAdapter<CreatePostRequest>(new CreatePostRequestValidator()),
            postsRepository: fakePostsRepository,
            getCurrentTime: () => CurrentTime);

        _commentsService = new CommentsService(
            getCurrentTime: () => CurrentTime, 
            commentsRepository: new FakeCommentsRepository(fakePostsRepository),
            postsRepository: fakePostsRepository);
    }

    protected (PostResponse?, Errors?) CreatePost(CreatePostRequest request) => 
        _postsService.Create(request);

    protected PostResponse GetPostById(int id) => 
        _postsService.ById(id);

    protected void CreateComment(CreateCommentRequest request) => 
        _commentsService.Create(request);
}

public class FakeCommentsRepository : FakeRepository<Comment>
{
    private readonly IRepository<Post> _posts;

    public FakeCommentsRepository(IRepository<Post> posts) => 
        _posts = posts;

    public override void Add(Comment entity)
    {
        base.Add(entity);
        var post = _posts.ById(entity.Post.Id);
        entity.PostId = entity.Post.Id;
        entity.Post = post;
        post.Comments.Add(entity);
    }
}