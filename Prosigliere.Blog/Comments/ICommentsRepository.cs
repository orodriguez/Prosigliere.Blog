using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Comments;

public interface ICommentsRepository : IRepository<Comment>
{
    IEnumerable<Comment> ByPost(int postId);
}