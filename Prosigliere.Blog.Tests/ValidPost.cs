using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.Tests;

public record ValidPost() : CreatePostRequest(ValidTitle, ValidContent)
{
    public const string ValidTitle = "Good News";

    public const string ValidContent = @"
                Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                Nunc dignissim dui arcu, eget tincidunt elit ultrices id.";
}