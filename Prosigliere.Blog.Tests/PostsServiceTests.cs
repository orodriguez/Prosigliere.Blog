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

        var response = CreatePost(new(
            ValidCreatePostRequest.ValidTitle,
            ValidCreatePostRequest.ValidContent));

        // TODO: Create custom assertion
        var blogPost = Assert.IsType<Response<PostResponse>.Success>(response).Value;

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
        var response = CreatePost(new ValidCreatePostRequest { Title = title });

        var errors = Assert.IsType<Response<PostResponse>.ValidationErrors>(response).Errors;
        var error = Assert.Single(errors[nameof(CreatePostRequest.Title)]);
        Assert.Equal(expectedError, error);
    }

    [Theory]
    [InlineData(null, "'Content' must not be empty.")]
    [InlineData("", "'Content' must not be empty.")]
    public void Create_ContentValidations(string content, string expectedError)
    {
        var response = CreatePost(new ValidCreatePostRequest { Content = content });
        
        var errors = Assert.IsType<Response<PostResponse>.ValidationErrors>(response).Errors;
        var error = Assert.Single(errors[nameof(CreatePostRequest.Content)]);
        Assert.Equal(expectedError, error);
    }

    [Fact]
    public void ById()
    {
        var createPostResponse = CreatePost(new ValidCreatePostRequest());

        var createdPost = Assert.IsType<Response<PostResponse>.Success>(createPostResponse).Value;
        
        var retrievePostResponse = GetPostById(createdPost.Id);
        
        var retrievedPost = Assert.IsType<Response<PostResponse>.Success>(retrievePostResponse).Value;
        
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
        var createResponse = CreatePost(new ValidCreatePostRequest());

        var createdPost = Assert.IsType<Response<PostResponse>.Success>(createResponse).Value;

        CreateComment(new ValidCreateCommentRequest(PostId: createdPost.Id));

        var retrievedResponse = GetPostById(createdPost.Id);

        var retrievePost = Assert.IsType<Response<PostResponse>.Success>(retrievedResponse).Value;
        
        var comment = Assert.Single(retrievePost.Comments);

        Assert.Equal(ValidCreateCommentRequest.ValidContent, comment.Content);
        Assert.Equal(CurrentTime, comment.CreatedAt);
    }

    // TODO: CommentsServiceTests.Create
    // TODO: CommentsServiceTests.Create PostId NotFound
    // TODO: CommentsServiceTests.Create_ValidationErrors
}