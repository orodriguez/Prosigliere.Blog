using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Validations;
using static Prosigliere.Blog.Result;

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

    public Result<CommentResponse> Create(int postId, CreateCommentRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Any())
            return ValidationErrors<CommentResponse>(errors);
        
        var post = _postsRepository.ById(postId);
        if (post == null)
            return RecordNotFound<CommentResponse>(
                CreatePostNotFoundErrorMessage(postId, request));
        
        var comment = CreateComment(request, post);

        _commentsRepository.Add(comment);

        return Success(CreateResponse(comment));
    }

    private Comment CreateComment(CreateCommentRequest request, Post post) =>
        new()
        {
            Post = post,
            Content = request.Content,
            CreatedAt = _getCurrentTime()
        };

    private static CommentResponse CreateResponse(Comment comment) =>
        new(
            Id: comment.Id,
            PostId: comment.PostId,
            Content: comment.Content, 
            CreatedAt: comment.CreatedAt);

    private static string CreatePostNotFoundErrorMessage(int postId, CreateCommentRequest request) => 
        $"Unable to add comment: Post with {nameof(postId)} = {postId} can not be found.";
}