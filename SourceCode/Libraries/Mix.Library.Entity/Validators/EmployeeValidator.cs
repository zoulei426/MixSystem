using FluentValidation;
using Microsoft.Extensions.Localization;
using Mix.Library.Entities.Dtos;

namespace Mix.Library.Entities.Validators
{
    /// <summary>
    /// EmployeeAddDtoValidator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{T}" />
    public class EmployeeAddDtoValidator : AbstractValidator<EmployeeAddDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeAddDtoValidator"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        public EmployeeAddDtoValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);
        }
    }

    /// <summary>
    /// EmployeeUpdateDtoValidator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{T}" />
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeUpdateDtoValidator"/> class.
        /// </summary>
        /// <param name="localizer">The localizer.</param>
        public EmployeeUpdateDtoValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);
        }
    }
}