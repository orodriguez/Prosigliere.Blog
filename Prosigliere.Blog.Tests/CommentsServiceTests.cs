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

        var createdComment = CreateComment(createdPost.Id, new(
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

        var errors = CreateComment(createdPost.Id, new(Content: content))
            .AssertValidationErrors();

        var error = Assert.Single(errors[nameof(CreateCommentRequest.Content)]);
        Assert.Equal(expectedError, error);
    }

    [Fact]
    public void Create_PostNotFound()
    {
        const int unknownPostId = 42;
        var error = CreateComment(unknownPostId, new ValidCreateCommentRequest())
            .AssertRecordNotFound();

        Assert.Equal("Unable to add comment: Post with postId = 42 can not be found.", error);
    }
}