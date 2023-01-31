using Pronia.Areas.Admin.ViewModels.Slider;
using Pronia.Contracts.SliderImage;
using Pronia.Validators;
using FluentValidation;

namespace Pronia.Areas.Admin.Validators.Slider
{
    public class AddViewModelValidator :   AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Image)
               .Cascade(CascadeMode.Stop)

               .NotNull()
               .WithMessage("Image can't be empty")

               .SetValidator(
                    new FileValidator(2, FileSizes.Giga,
                        FileExtensions.JPG.GetExtension(), FileExtensions.PNG.GetExtension())!);
        }


    }
}
