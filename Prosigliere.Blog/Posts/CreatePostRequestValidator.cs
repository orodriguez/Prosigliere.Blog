using FluentValidation;
using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.Posts;

public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    private const int MaximumTitleLength = 80;
    private const int MaximumContentLength = 100_000;

    public CreatePostRequestValidator()
    {
        RuleFor(post => post.Title)
            .NotEmpty()
            .MaximumLength(MaximumTitleLength);

        RuleFor(post => post.Content)
            .NotEmpty()
            .MaximumLength(MaximumContentLength);
    }
}