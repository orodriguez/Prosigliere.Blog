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
    
    // TODO: CommentsServiceTests.Create PostId NotFound
    // TODO: CommentsServiceTests.Create_ValidationErrors
}