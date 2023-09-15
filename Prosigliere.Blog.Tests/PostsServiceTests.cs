using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;
using Xunit;

namespace Prosigliere.Blog.Tests;

public class PostsServiceTests
{
    private readonly IPostsService _service;
    private DateTime _currentTime = DateTime.Now;
    private readonly FakeRepository<Post> _fakePostsRepository;

    public PostsServiceTests()
    {
        _fakePostsRepository = new FakeRepository<Post>();
        
        _service = new PostsService(
            new FluentValidatorAdapter<CreatePostRequest>(new CreatePostRequestValidator()),
            blogPostsRepository: _fakePostsRepository,
            currentTime: () => _currentTime);
    }

    [Fact]
    public void Create()
    {
        _currentTime = DateTime.Parse("2023-09-15 5:00PM");

        var (blogPost, _) = _service.Create(new(
            ValidPost.ValidTitle,
            ValidPost.ValidContent));
        
        Assert.NotNull(blogPost);
        Assert.Equal(1, blogPost.Id);
        Assert.Equal(ValidPost.ValidTitle, blogPost.Title);
        Assert.Equal(ValidPost.ValidContent, blogPost.Content);
        Assert.Equal(_currentTime, blogPost.CreatedAt);
        
        Assert.NotNull(_fakePostsRepository.ById(1));
    }
    
    [Theory]
    [InlineData("", "'Title' must not be empty.")]
    [InlineData(null, "'Title' must not be empty.")]
    [InlineData(
        "This is a very long title, it should not be valid, because it is toooooooooooooooooooo long", 
        "The length of 'Title' must be 80 characters or fewer. You entered 91 characters.")]
    public void Create_TitleValidations(string title, string expectedError)
    {
        var (_, errors) = _service.Create(new ValidPost { Title = title });
        
        Assert.NotNull(errors);
        var error = Assert.Single(errors[nameof(CreatePostRequest.Title)]);
        Assert.Equal(expectedError, error);
    }
    
    [Theory]
    [InlineData(null, "'Content' must not be empty.")]
    [InlineData("", "'Content' must not be empty.")]
    public void Create_ContentValidations(string content, string expectedError)
    {
        var (_, errors) = _service.Create(new ValidPost { Content = content });

        Assert.NotNull(errors);
        var error = Assert.Single(errors[nameof(CreatePostRequest.Content)]);
        Assert.Equal(expectedError, error);
    }
}