namespace Prosigliere.Blog.Api.Posts;

public interface IPostsService
{
    Result<DetailedPostResponse> Create(CreatePostRequest request);
    Result<DetailedPostResponse> ById(int id);
    Result<IEnumerable<ShortPostResponse>> Get();
}