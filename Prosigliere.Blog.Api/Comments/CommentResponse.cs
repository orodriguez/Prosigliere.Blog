namespace Prosigliere.Blog.Api.Comments;

public record CommentResponse(
    int Id, 
    int PostId, 
    string Content, 
    DateTime CreatedAt);