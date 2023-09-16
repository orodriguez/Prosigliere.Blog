namespace Prosigliere.Blog.Api.Posts;

public interface IPostsService
{
    Result<PostResponse> Create(CreatePostRequest request);
    Result<PostResponse> ById(int id);
}