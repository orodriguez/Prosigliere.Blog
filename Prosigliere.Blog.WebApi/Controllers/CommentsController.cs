using Microsoft.AspNetCore.Mvc;
using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Comments;

namespace Prosigliere.Blog.WebApi.Controllers;

[ApiController]
[Route("posts/{postId}/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _service;

    public CommentsController(ICommentsService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Errors))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public ActionResult<CommentResponse> Post([FromRoute] int postId, [FromBody] CreateCommentRequest request) =>
        _service.Create(postId, request).ToActionResult();
    
    // TODO: Get All Comments
}