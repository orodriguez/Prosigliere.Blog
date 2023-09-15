namespace Prosigliere.Blog.Api.Posts;

public interface IPostsService
{
    PostResponse Create(CreatePostRequest request);
}