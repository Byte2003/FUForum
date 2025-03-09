using FluentValidation;

namespace FUForum.ViewModels.Contents;

public class KnowledgeBaseCreateRequestValidator : AbstractValidator<KnowledgeBaseCreateRequest>
{
    public KnowledgeBaseCreateRequestValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.SeoAlias).NotEmpty().WithMessage("SeoAlias is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Environment).NotEmpty().WithMessage("Environment is required");
        RuleFor(x => x.Problem).NotEmpty().WithMessage("Problem is required");
        RuleFor(x => x.StepToReproduce).NotEmpty().WithMessage("StepToReproduce is required");
        RuleFor(x => x.ErrorMessage).NotEmpty().WithMessage("ErrorMessage is required");
        RuleFor(x => x.Workaround).NotEmpty().WithMessage("Workaround is required");
        RuleFor(x => x.Note).NotEmpty().WithMessage("Note is required");
       
    }
}