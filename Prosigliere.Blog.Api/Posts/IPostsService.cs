namespace Prosigliere.Blog.Api.Posts;

public interface IPostsService
{
    (PostResponse?, Errors?) Create(CreatePostRequest request);
}