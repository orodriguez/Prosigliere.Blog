using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Tests;

public class FakeCommentsRepository : FakeRepository<Comment>
{
    private readonly IRepository<Post> _posts;

    public FakeCommentsRepository(IRepository<Post> posts) => 
        _posts = posts;

    public override void Add(Comment entity)
    {
        base.Add(entity);
        var post = _posts.ById(entity.Post.Id);
        entity.PostId = entity.Post.Id;
        entity.Post = post;
        post.Comments.Add(entity);
    }
}