using FluentValidation;
using Microsoft.Extensions.Localization;
using Mix.Library.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Validators
{
    public class CompanyAddDtoValidator : AbstractValidator<CompanyAddDto>
    {
        public CompanyAddDtoValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(x => localizer["Field is Required"]);
        }
    }
}