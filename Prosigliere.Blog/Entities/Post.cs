using Prosigliere.Blog.Api.Comments;

namespace Prosigliere.Blog.Entities;

public class Post : IEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public Post() => Comments = new List<Comment>();
    public int CommentsCount() => Comments.Count;
}