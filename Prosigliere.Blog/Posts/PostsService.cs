using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Validations;

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
            return new Result<PostResponse>.ValidationErrors(errors);
        
        var post = new Post
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = _getCurrentTime(),
        };

        _postsRepository.Add(post);

        return new Result<PostResponse>.Success(new PostResponse(
            Id: post.Id,
            Title: post.Title,
            Content: post.Content,
            CreatedAt: post.CreatedAt,
            Comments: post.Comments.Select(CreatePostResponseComment)));
    }

    public Result<PostResponse> ById(int id)
    {
        var post = _postsRepository.ById(id);

        if (post == null)
            return new Result<PostResponse>.RecordNotFound(
                $"Post with PostId = {id} can not be found.");
        
        return new Result<PostResponse>.Success(CreateDetailedResponse(post));
    }

    public Result<IEnumerable<ShortPostResponse>> Get()
    {
        var posts = _postsRepository.All()
            .Select(CreateShortResponse);

        return new Result<IEnumerable<ShortPostResponse>>.Success(posts);
    }

    private static ShortPostResponse CreateShortResponse(Post post) =>
        new(
            Id: post.Id, 
            Title: post.Title, 
            CreatedAt: post.CreatedAt, 
            CommentsCount: post.CommentsCount());

    private static PostResponse CreateDetailedResponse(Post? post) =>
        new(
            Id: post.Id, 
            Title: post.Title, 
            Content: post.Content, 
            CreatedAt: post.CreatedAt,
            Comments: post.Comments.Select(CreatePostResponseComment));

    private static PostResponse.Comment CreatePostResponseComment(Comment c) => 
        new(Id: c.Id, Content: c.Content, CreatedAt: c.CreatedAt);
}