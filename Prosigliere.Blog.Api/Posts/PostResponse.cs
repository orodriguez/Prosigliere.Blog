namespace Prosigliere.Blog.Api.Posts;

public record PostResponse(int Id, string Title, string Content, DateTime CreatedAt);