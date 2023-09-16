namespace Prosigliere.Blog.Api.Comments;

public record CreateCommentResponse(
    int Id, 
    int PostId, 
    string Content, 
    DateTime CreatedAt);