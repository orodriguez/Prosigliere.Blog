using System.Runtime.CompilerServices;
using Prosigliere.Blog.Api.Comments;

namespace Prosigliere.Blog.Api.Posts;

public record PostResponse(
    int Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    IEnumerable<PostResponse.Comment> Comments)
{
    public record Comment(int Id, 
        string Content, 
        DateTime CreatedAt);
};