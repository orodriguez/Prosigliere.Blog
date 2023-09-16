namespace Prosigliere.Blog.Api.Posts;

public record DetailedPostResponse(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    IEnumerable<DetailedPostResponse.Comment> Comments)
{
    public record Comment(int Id, 
        string Content, 
        DateTime CreatedAt);
};