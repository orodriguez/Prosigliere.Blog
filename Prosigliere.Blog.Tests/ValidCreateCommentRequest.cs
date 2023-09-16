using System.Runtime.Intrinsics.Arm;
using Prosigliere.Blog.Api.Comments;

namespace Prosigliere.Blog.Tests;

public record ValidCreateCommentRequest(int PostId) : CreateCommentRequest(PostId, ValidContent)
{
    public const string ValidContent =
        "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...";
}