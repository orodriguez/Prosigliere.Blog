using Prosigliere.Blog.Api.Posts;
using Prosigliere.Blog.Tests.Samples;
using Xunit;

namespace Prosigliere.Blog.Tests;

public class PostsServiceTests : AbstractServiceTests
{
    [Fact]
    public void Create()
    {
        CurrentTime = DateTime.Parse("2023-09-15 5:00PM");
        
        var createdPost = CreatePost(new(
                ValidCreatePostRequest.ValidTitle,
                ValidCreatePostRequest.ValidContent)
            ).AssertSuccess();

        Assert.Equal(1, createdPost.Id);
        Assert.Equal(ValidCreatePostRequest.ValidTitle, createdPost.Title);
        Assert.Equal(ValidCreatePostRequest.ValidContent, createdPost.Content);
        Assert.Equal(CurrentTime, createdPost.CreatedAt);
    }

    [Theory]
    [InlineData("", "'Title' must not be empty.")]
    [InlineData(null, "'Title' must not be empty.")]
    [InlineData(
        "This is a very long title, it should not be valid, because it is toooooooooooooooooooo long",
        "The length of 'Title' must be 80 characters or fewer. You entered 91 characters.")]
    public void Create_TitleValidations(string title, string expectedError)
    {
        var errors = CreatePost(new ValidCreatePostRequest { Title = title })
            .AssertValidationErrors();
        
        var error = Assert.Single(errors[nameof(CreatePostRequest.Title)]);
        Assert.Equal(expectedError, error);
    }

    [Theory]
    [InlineData(null, "'Content' must not be empty.")]
    [InlineData("", "'Content' must not be empty.")]
    public void Create_ContentValidations(string content, string expectedError)
    {
        var errors = CreatePost(new ValidCreatePostRequest { Content = content })
            .AssertValidationErrors();
        
        var error = Assert.Single(errors[nameof(CreatePostRequest.Content)]);
        Assert.Equal(expectedError, error);
    }

    [Fact]
    public void ById()
    {
        var createdPost = CreatePost(new ValidCreatePostRequest()).AssertSuccess();

        var retrievedPost = GetPostById(createdPost.Id).AssertSuccess();
        
        Assert.NotNull(retrievedPost);
        Assert.Equal(createdPost.Id, retrievedPost.Id);
        Assert.Equal(ValidCreatePostRequest.ValidTitle, retrievedPost.Title);
        Assert.Equal(ValidCreatePostRequest.ValidContent, retrievedPost.Content);
        Assert.Equal(CurrentTime, retrievedPost.CreatedAt);
        Assert.Empty(retrievedPost.Comments);
    }
    
    [Fact]
    public void ById_PostNotFound()
    {
        const int unknownPostId = 42;
        var error = GetPostById(unknownPostId)
            .AssertRecordNotFound();

        Assert.Equal("Post with PostId = 42 can not be found.", error);
    }

    [Fact]
    public void ById_WithComments()
    {
        var createdPost = CreatePost(new ValidCreatePostRequest()).AssertSuccess();

        CreateComment(createdPost.Id, new ValidCreateCommentRequest()).AssertSuccess();

        var retrievedPost = GetPostById(createdPost.Id).AssertSuccess();

        var comment = Assert.Single(retrievedPost.Comments);

        Assert.Equal(ValidCreateCommentRequest.ValidContent, comment.Content);
        Assert.Equal(CurrentTime, comment.CreatedAt);
    }

    [Fact]
    public void Get()
    {
        CreatePost(new ValidCreatePostRequest { Title = "Post 1" }).AssertSuccess();
        CreatePost(new ValidCreatePostRequest { Title = "Post 2" }).AssertSuccess();

        var allPosts = GetPosts().AssertSuccess().ToArray();
        
        Assert.Equal(2, allPosts.Length);

        var firstPost = allPosts.First();
        
        Assert.Equal(1, firstPost.Id);
        Assert.Equal("Post 1", firstPost.Title);
        Assert.Equal(CurrentTime, firstPost.CreatedAt);
        Assert.Equal(0, firstPost.CommentsCount);
    }
    
    [Fact]
    public void Get_WithComments()
    {
        var createdPost = CreatePost(new ValidCreatePostRequest { Title = "Post 1" }).AssertSuccess();

        foreach (var _ in Enumerable.Range(1, 3))
            CreateComment(createdPost.Id, new ValidCreateCommentRequest()).AssertSuccess();

        var allPosts = GetPosts().AssertSuccess().ToArray();
        
        var post = Assert.Single(allPosts);
        Assert.Equal(3, post.CommentsCount);
    }
}