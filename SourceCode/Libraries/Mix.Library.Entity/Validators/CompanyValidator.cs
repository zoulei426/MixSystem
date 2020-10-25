using FluentValidation;
using Microsoft.Extensions.Localization;
using Mix.Library.Entities.Dtos;

namespace Mix.Library.Entities.Validators
{
    /// <summary>
    /// CompanyAddDtoValidator
    /// </summary>
    /// <seealso cref="AbstractValidator{T}" />
    public class CompanyAddDtoValidator : AbstractValidator<CompanyAddDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyAddDtoValidator"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        public CompanyAddDtoValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);
        }
    }
}