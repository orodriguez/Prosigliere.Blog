using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Entities;
using Prosigliere.Blog.Posts;
using Xunit;

namespace Prosigliere.Blog.Tests;

public class PostsServiceTests
{
    private readonly IPostsService _service;
    private DateTime _currentTime = DateTime.Now;

    public PostsServiceTests() =>
        _service = new PostsService(
            blogPostsRepository: new FakeRepository<Post>(), 
            currentTime: () => _currentTime);

    [Fact]
    public void Create()
    {
        const string title = "Good News";
        const string content = @"
                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                Nunc dignissim dui arcu, eget tincidunt elit ultrices id.";
        _currentTime = DateTime.Parse("2023-09-15 5:00PM");

        var blogPost = _service.Create(new(
            (string)title,
            (string)content));

        Assert.Equal(1, blogPost.Id);
        Assert.Equal(title, blogPost.Title);
        Assert.Equal(content, blogPost.Content);
        Assert.Equal(_currentTime, blogPost.CreatedAt);
    }
}