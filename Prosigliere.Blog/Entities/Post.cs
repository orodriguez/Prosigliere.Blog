namespace Prosigliere.Blog.Entities;

public class Post : IEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}