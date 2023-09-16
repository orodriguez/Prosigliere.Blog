using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Validations;
using static Prosigliere.Blog.Result;

namespace Prosigliere.Blog.Posts;

public class PostsService : IPostsService
{
    private readonly IValidator<CreatePostRequest> _validator;
    private readonly IRepository<Post> _postsRepository;
    private readonly Func<DateTime> _getCurrentTime;

    public PostsService(
        IValidator<CreatePostRequest> validator, 
        Func<DateTime> getCurrentTime,
        IRepository<Post> postsRepository)
    {
        _validator = validator;
        _getCurrentTime = getCurrentTime;
        _postsRepository = postsRepository;
    }

    public Result<PostResponse> Create(CreatePostRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Any())
            return ValidationErrors<PostResponse>(errors);
        
        var post = CreatePost(request);

        _postsRepository.Add(post);

        return Success(CreateResponse(post));
    }

    private Post CreatePost(CreatePostRequest request) =>
        new()
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = _getCurrentTime(),
        };

    private static PostResponse CreateResponse(Post post) =>
        new(
            Id: post.Id,
            Title: post.Title,
            Content: post.Content,
            CreatedAt: post.CreatedAt,
            Comments: post.Comments.Select(CreatePostResponseComment));

    public Result<PostResponse> ById(int id)
    {
        var post = _postsRepository.ById(id);
        if (post == null)
            return RecordNotFound<PostResponse>(
                $"Post with PostId = {id} can not be found.");
        
        return Success(CreateDetailedResponse(post));
    }

    public Result<IEnumerable<ShortPostResponse>> Get()
    {
        var posts = _postsRepository.All()
            .Select(CreateShortResponse);

        return Success(posts);
    }

    private static ShortPostResponse CreateShortResponse(Post post) =>
        new(
            Id: post.Id, 
            Title: post.Title, 
            CreatedAt: post.CreatedAt, 
            CommentsCount: post.CommentsCount());

    private static PostResponse CreateDetailedResponse(Post post) =>
        new(
            Id: post.Id, 
            Title: post.Title, 
            Content: post.Content, 
            CreatedAt: post.CreatedAt,
            Comments: post.Comments.Select(CreatePostResponseComment));

    private static PostResponse.Comment CreatePostResponseComment(Comment c) => 
        new(Id: c.Id, Content: c.Content, CreatedAt: c.CreatedAt);
}