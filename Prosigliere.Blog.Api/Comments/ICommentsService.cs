namespace Prosigliere.Blog.Api.Comments;

public interface ICommentsService
{
    Response<CreateCommentResponse> Create(CreateCommentRequest request);
}