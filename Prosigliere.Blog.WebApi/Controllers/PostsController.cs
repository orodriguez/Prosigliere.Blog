using Microsoft.AspNetCore.Mvc;
using Prosigliere.Blog.Api;
using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostsService _service;

    public PostsController(IPostsService service) => _service = service;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedPostResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Errors))]
    public ActionResult<DetailedPostResponse> Post(CreatePostRequest request) =>
        _service.Create(request).ToActionResult();

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailedPostResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public ActionResult<DetailedPostResponse> Get(int id) =>
        _service.ById(id).ToActionResult();

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ShortPostResponse>))]
    public ActionResult<IEnumerable<ShortPostResponse>> Get() =>
        _service.Get().ToActionResult();
}