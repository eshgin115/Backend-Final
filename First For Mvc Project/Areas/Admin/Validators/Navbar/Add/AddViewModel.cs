using Pronia.Areas.Admin.ViewModels.Navbar;
using FluentValidation;

namespace Pronia.Areas.Admin.Validators.Navbar.Add
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Name)
                .NotNull()
                .WithMessage("Name can't be empty")
                .NotEmpty()
                .WithMessage("Name can't be empty")
                .MinimumLength(3)
                .WithMessage("Minimum length should be 3")
                .MaximumLength(15)
                .WithMessage("Maximum length should be 15");
            RuleFor(avm => avm.ToURL)
            .NotNull()
            .WithMessage("ToURL can't be empty")
            .NotEmpty()
            .WithMessage("ToURL can't be empty")
            .MinimumLength(10)
            .WithMessage("Minimum length should be 10")
            .MaximumLength(35)
            .WithMessage("Maximum length should be 35");
        }
    }
}
