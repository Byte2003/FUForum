using FluentValidation;

namespace FUForum.ViewModels.Contents;

public class CommentCreateRequestValidator : AbstractValidator<CommentCreateRequest>
{
    public CommentCreateRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
        RuleFor(x => x.Content).MaximumLength(500).WithMessage("Content can not over 500 characters");
        RuleFor(x => x.KnowledgeBaseId).NotEmpty().WithMessage("KnowledgeBaseId is required");
    }
}