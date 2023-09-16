using Prosigliere.Blog.Api.Comments;
using Prosigliere.Blog.Tests.Samples;
using Xunit;

namespace Prosigliere.Blog.Tests;

public class CommentsServiceTests : AbstractServiceTests
{
    [Fact]
    public void Create()
    {
        var createdPost = CreatePost(new ValidCreatePostRequest()).AssertSuccess();

        var createdComment = CreateComment(new(
            PostId: createdPost.Id,
            Content: ValidCreateCommentRequest.ValidContent)).AssertSuccess();

        Assert.Equal(1, createdComment.Id);
        Assert.Equal(createdPost.Id, createdComment.PostId);
        Assert.Equal(ValidCreateCommentRequest.ValidContent, createdComment.Content);
        Assert.Equal(CurrentTime, createdComment.CreatedAt);
    }

    [Theory]
    [InlineData(null, "'Content' must not be empty.")]
    [InlineData("", "'Content' must not be empty.")]
    public void Create_ContentValidations(string content, string expectedError)
    {
        var createdPost = CreatePost(new ValidCreatePostRequest()).AssertSuccess();

        var errors = CreateComment(new(
            PostId: createdPost.Id,
            Content: content)).AssertValidationErrors();

        var error = Assert.Single(errors[nameof(CreateCommentRequest.Content)]);
        Assert.Equal(expectedError, error);
    }
    
    [Fact]
    public void Create_PostIdZero()
    {
        var errors = CreateComment(new ValidCreateCommentRequest(PostId: 0))
            .AssertValidationErrors();

        var error = Assert.Single(errors[nameof(CreateCommentRequest.PostId)]);
        Assert.Equal("'Post Id' must not be empty.", error);
    }
    
    [Fact]
    public void Create_PostNotFound()
    {
        const int unknownPostId = 42;
        var error = CreateComment(new ValidCreateCommentRequest(PostId: unknownPostId))
            .AssertRecordNotFound();

        Assert.Equal("Unable to add comment: Post with PostId = 42 can not be found.", error);
    }
    
    // TODO: CommentsServiceTests.Create_ValidationErrors
}