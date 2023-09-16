using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public class CommentsRepository : Repository<Comment>, ICommentsRepository
{
    public CommentsRepository(ProsigliereBlogDbContext db) : base(db)
    {
    }

    public IEnumerable<Comment> ByPost(int postId) => 
        _db.Comments.Where(comment => comment.PostId == postId);
}