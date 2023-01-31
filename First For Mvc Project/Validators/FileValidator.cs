using Pronia.Contracts.SliderImage;
using FluentValidation;

namespace Pronia.Validators
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator() { }

        public FileValidator(long size, FileSizes fileSize, params string[] extensions)
        {
            RuleFor(i => i.Length)
                .LessThanOrEqualTo(size * fileSize.GetSize())
                .WithMessage($"Image size should be less that : {size} {fileSize.GetShortNameWithByte()}");

            RuleFor(i => i.FileName)
                .Must(fn => extensions.Contains(Path.GetExtension(fn)))
                .WithMessage($"Image extension shold be {string.Join(", ", extensions)}");
        }
    }
}
