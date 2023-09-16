namespace Prosigliere.Blog.Api.Comments;

public interface ICommentsService
{
    CreateCommentResponse Create(CreateCommentRequest request);
}