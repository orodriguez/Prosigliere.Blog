namespace Prosigliere.Blog.Api.Comments;

public record CreateCommentRequest(int PostId, string Content);