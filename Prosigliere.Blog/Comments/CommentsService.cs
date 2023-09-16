using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Comments;

public class CommentsService : ICommentsService
{
    private readonly Func<DateTime> _getCurrentTime;
    private readonly IRepository<Comment> _commentsRepository;
    private readonly IRepository<Post> _postsRepository;

    public CommentsService(
        Func<DateTime> getCurrentTime, 
        IRepository<Comment> commentsRepository, 
        IRepository<Post> postsRepository)
    {
        _getCurrentTime = getCurrentTime;
        _commentsRepository = commentsRepository;
        _postsRepository = postsRepository;
    }

    public Response<CreateCommentResponse> Create(CreateCommentRequest request)
    {
        var post = _postsRepository.ById(request.PostId);
        var comment = new Comment
        {
            Post = post,
            Content = request.Content,
            CreatedAt = _getCurrentTime()
        };

        _commentsRepository.Add(comment);

        return new Response<CreateCommentResponse>.Success(new(
            Id: comment.Id,
            PostId: request.PostId,
            Content: comment.Content, 
            CreatedAt: comment.CreatedAt));
    }
}