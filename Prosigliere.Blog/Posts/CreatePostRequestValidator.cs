using FluentValidation;
using Prosigliere.Blog.Api.Posts;

namespace Prosigliere.Blog.Posts;

public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(post => post.Title)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(post => post.Content)
            .NotEmpty();
    }
}