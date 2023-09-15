using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Posts;

public class PostsService : IPostsService
{
    private readonly IValidator<CreatePostRequest> _validator;
    private readonly IRepository<Post> _blogPostsRepository;
    private readonly Func<DateTime> _currentTime;

    public PostsService(
        IValidator<CreatePostRequest> validator, 
        Func<DateTime> currentTime,
        IRepository<Post> blogPostsRepository)
    {
        _validator = validator;
        _currentTime = currentTime;
        _blogPostsRepository = blogPostsRepository;
    }

    public (PostResponse?, Errors?) Create(CreatePostRequest request)
    {
        var errors = _validator.Validate(request);

        if (errors.Any())
            return (null, errors);
        
        var blogPost = new Post
        {
            Title = request.Title,
            Content = request.Content
        };

        _blogPostsRepository.Add(blogPost);

        return (new PostResponse(
            Id: blogPost.Id,
            Title: blogPost.Title,
            Content: blogPost.Content,
            CreatedAt: _currentTime()), null);
    }
}