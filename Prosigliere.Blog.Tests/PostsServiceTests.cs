using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;
using Xunit;

namespace Prosigliere.Blog.Tests;

public class PostsServiceTests : AbstractServiceTests
{
    [Fact]
    public void Create()
    {
        CurrentTime = DateTime.Parse("2023-09-15 5:00PM");
        
        var (blogPost, _) = CreatePost(new(
            ValidCreatePostRequest.ValidTitle,
            ValidCreatePostRequest.ValidContent));
        
        Assert.NotNull(blogPost);
        Assert.Equal(1, blogPost.Id);
        Assert.Equal(ValidCreatePostRequest.ValidTitle, blogPost.Title);
        Assert.Equal(ValidCreatePostRequest.ValidContent, blogPost.Content);
        Assert.Equal(CurrentTime, blogPost.CreatedAt);
    }

    [Theory]
    [InlineData("", "'Title' must not be empty.")]
    [InlineData(null, "'Title' must not be empty.")]
    [InlineData(
        "This is a very long title, it should not be valid, because it is toooooooooooooooooooo long", 
        "The length of 'Title' must be 80 characters or fewer. You entered 91 characters.")]
    public void Create_TitleValidations(string title, string expectedError)
    {
        var (_, errors) = CreatePost(new ValidCreatePostRequest { Title = title });
        
        Assert.NotNull(errors);
        var error = Assert.Single(errors[nameof(CreatePostRequest.Title)]);
        Assert.Equal(expectedError, error);
    }
    
    [Theory]
    [InlineData(null, "'Content' must not be empty.")]
    [InlineData("", "'Content' must not be empty.")]
    public void Create_ContentValidations(string content, string expectedError)
    {
        var (_, errors) = CreatePost(new ValidCreatePostRequest { Content = content });

        Assert.NotNull(errors);
        var error = Assert.Single(errors[nameof(CreatePostRequest.Content)]);
        Assert.Equal(expectedError, error);
    }

    [Fact]
    public void ById()
    {
        var (createdPost, _) = CreatePost(new ValidCreatePostRequest());

        Assert.NotNull(createdPost);
        var retrievedPost = GetPostById(createdPost.Id);
        
        Assert.NotNull(retrievedPost);
        Assert.Equal(createdPost.Id, retrievedPost.Id);
        Assert.Equal(ValidCreatePostRequest.ValidTitle, retrievedPost.Title);
        Assert.Equal(ValidCreatePostRequest.ValidContent, retrievedPost.Content);
        Assert.Equal(CurrentTime, retrievedPost.CreatedAt);
        Assert.Empty(retrievedPost.Comments);
    }

    [Fact]
    public void ById_WithComments()
    {
        var (createdPost, _) = CreatePost(new ValidCreatePostRequest());
        
        Assert.NotNull(createdPost);
        
        CreateComment(new ValidCreateCommentRequest(PostId: createdPost.Id));

        var retrievedPost = GetPostById(createdPost.Id);

        var comment = Assert.Single(retrievedPost.Comments);
        
        Assert.Equal(ValidCreateCommentRequest.ValidContent, comment.Content);
        Assert.Equal(CurrentTime, comment.CreatedAt);
    }
    
    // TODO: Test ById NotFound
    
    // TODO: CommentsServiceTests.Create
    // TODO: CommentsServiceTests.Create PostId NotFound
    // TODO: CommentsServiceTests.Create_ValidationErrors
}