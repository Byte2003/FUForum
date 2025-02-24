
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUForum.ViewModels.Systems
{
    public class RoleCreateRequestValidator : AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required")
                .MaximumLength(50).WithMessage("Id cannot over limit 50 characters");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name cannot over limit 50 characters");
        }
    }
}
