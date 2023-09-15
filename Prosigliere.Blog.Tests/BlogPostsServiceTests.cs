namespace Prosigliere.Blog.Tests;

public class BlogPostsServiceTests
{
    private readonly IBlogPostsService _service;
    private DateTime _currentTime = DateTime.Now;

    public BlogPostsServiceTests() =>
        _service = new BlogPostsService(
            blogPostsRepository: new FakeRepository<BlogPostEntity>(), 
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
            Title: title,
            Content: content));

        Assert.Equal(1, blogPost.Id);
        Assert.Equal(title, blogPost.Title);
        Assert.Equal(content, blogPost.Content);
        Assert.Equal(_currentTime, blogPost.CreatedAt);
    }
}

public class FakeRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _entities = new List<T>();
    private int _nextId = 1;

    public void Add(T entity)
    {
        entity.Id = _nextId++;
        _entities.Add(entity);
    }
}

public interface IEntity
{
    public int Id { get; set; }
}

internal interface IBlogPostsService
{
    BlogPostResponse Create(CreateBlogPostRequest request);
}

public record CreateBlogPostRequest(string Title, string Content);

public record BlogPostResponse(int Id, string Title, string Content, DateTime CreatedAt);

public class BlogPostsService : IBlogPostsService
{
    private readonly IRepository<BlogPostEntity> _blogPostsRepository;
    private readonly Func<DateTime> _currentTime;

    public BlogPostsService(IRepository<BlogPostEntity> blogPostsRepository, Func<DateTime> currentTime)
    {
        _blogPostsRepository = blogPostsRepository;
        _currentTime = currentTime;
    }

    public BlogPostResponse Create(CreateBlogPostRequest request)
    {
        var blogPost = new BlogPostEntity
        {
            Title = request.Title,
            Content = request.Content
        };

        _blogPostsRepository.Add(blogPost);

        return new BlogPostResponse(
            Id: blogPost.Id,
            Title: blogPost.Title,
            Content: blogPost.Content,
            CreatedAt: _currentTime());
    }
}

public class BlogPostEntity : IEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}

public interface IRepository<T>
{
    void Add(T entity);
}