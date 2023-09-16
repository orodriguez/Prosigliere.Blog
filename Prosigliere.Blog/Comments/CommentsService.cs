using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Validations;

namespace Prosigliere.Blog.Comments;

public class CommentsService : ICommentsService
{
    private readonly IValidator<CreateCommentRequest> _validator;
    private readonly Func<DateTime> _getCurrentTime;
    private readonly IRepository<Comment> _commentsRepository;
    private readonly IRepository<Post> _postsRepository;

    public CommentsService(
        IValidator<CreateCommentRequest> validator, 
        Func<DateTime> getCurrentTime, 
        IRepository<Comment> commentsRepository, IRepository<Post> postsRepository)
    {
        _validator = validator;
        _getCurrentTime = getCurrentTime;
        _commentsRepository = commentsRepository;
        _postsRepository = postsRepository;
    }

    public Result<CreateCommentResponse> Create(CreateCommentRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Any())
            return new Result<CreateCommentResponse>.ValidationErrors(errors);
        
        var post = _postsRepository.ById(request.PostId);
        if (post == null)
            return new Result<CreateCommentResponse>.RecordNotFound(
                CreatePostNotFoundErrorMessage(request));
        
        var comment = new Comment
        {
            Post = post,
            Content = request.Content,
            CreatedAt = _getCurrentTime()
        };

        _commentsRepository.Add(comment);

        return new Result<CreateCommentResponse>.Success(new(
            Id: comment.Id,
            PostId: request.PostId,
            Content: comment.Content, 
            CreatedAt: comment.CreatedAt));
    }

    private static string CreatePostNotFoundErrorMessage(CreateCommentRequest request) => 
        $"Unable to add comment: Post with {nameof(CreateCommentRequest.PostId)} = {request.PostId} can not be found.";
}