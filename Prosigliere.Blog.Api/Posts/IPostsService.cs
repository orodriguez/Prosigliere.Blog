namespace Prosigliere.Blog.Api.Posts;

public interface IPostsService
{
    Response<PostResponse> Create(CreatePostRequest request);
    Response<PostResponse> ById(int id);
}