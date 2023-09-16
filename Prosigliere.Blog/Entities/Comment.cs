namespace Prosigliere.Blog.Entities;

public class Comment : IEntity
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int PostId { get; set; }
    public required Post Post { get; set; }
}