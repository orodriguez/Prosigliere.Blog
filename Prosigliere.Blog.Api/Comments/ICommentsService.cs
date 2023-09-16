namespace Prosigliere.Blog.Api.Comments;

public interface ICommentsService
{
    Result<CommentResponse> Create(int postId, CreateCommentRequest request);
}