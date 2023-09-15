using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Posts;

public class PostsService : IPostsService
{
    private readonly IRepository<Post> _blogPostsRepository;
    private readonly Func<DateTime> _currentTime;

    public PostsService(IRepository<Post> blogPostsRepository, Func<DateTime> currentTime)
    {
        _blogPostsRepository = blogPostsRepository;
        _currentTime = currentTime;
    }

    public PostResponse Create(CreatePostRequest request)
    {
        var blogPost = new Post
        {
            Title = request.Title,
            Content = request.Content
        };

        _blogPostsRepository.Add(blogPost);

        return new PostResponse(
            Id: blogPost.Id,
            Title: blogPost.Title,
            Content: blogPost.Content,
            CreatedAt: _currentTime());
    }
}