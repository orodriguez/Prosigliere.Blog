using FluentValidation;
using Prosigliere.Blog.Api.Comments;

namespace Prosigliere.Blog.Comments;

public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
{
    private const int ContentMaximumLength = 1_000;

    public CreateCommentRequestValidator()
    {
        RuleFor(request => request.PostId).NotEmpty();
        RuleFor(request => request.Content)
            .NotEmpty()
            .MaximumLength(ContentMaximumLength);
    }
}