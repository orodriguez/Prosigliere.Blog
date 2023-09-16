namespace Prosigliere.Blog.Api.Comments;

public interface ICommentsService
{
    Result<CreateCommentResponse> Create(CreateCommentRequest request);
}