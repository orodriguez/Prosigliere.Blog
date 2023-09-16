namespace Prosigliere.Blog.Api.Posts;

public record ShortPostResponse(int Id, string Title, DateTime CreatedAt, int CommentsCount);