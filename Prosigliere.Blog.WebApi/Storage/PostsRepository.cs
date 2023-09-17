using Microsoft.EntityFrameworkCore;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public class PostsRepository : Repository<Post>
{
    public PostsRepository(ProsigliereBlogDbContext db) : base(db)
    {
    }

    public override Post? ById(int id) =>
        Db.Posts
            .Include(post => post.Comments)
            .FirstOrDefault(post => post.Id == id);
}