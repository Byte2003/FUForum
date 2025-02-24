using FluentValidation;

namespace FUForum.ViewModels.Systems;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password has to atleast 8 characters")
            .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
            .WithMessage("Password is not match complexity rules.");
        ;
        
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
            .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email is not valid");
        
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(50)
            .WithMessage("FirstName cannot over 50 characters");
        
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required")
            .MaximumLength(50).WithMessage("LastName cannot over 50 characters");
        
        RuleFor(x => x.Dob).NotEmpty().WithMessage("Dob is required");
    }
}