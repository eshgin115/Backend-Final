using First_For_Mvc_Project.Areas.Admin.ViewModels.Slider;
using First_For_Mvc_Project.Contracts.SliderImage;
using First_For_Mvc_Project.Validators;
using FluentValidation;

namespace First_For_Mvc_Project.Areas.Admin.Validators.Slider
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
