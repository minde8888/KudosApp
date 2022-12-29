using FluentValidation;
using Kudos.Services.Dtos;

namespace Kudos.Services.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Surname).NotNull().NotEmpty()
                .WithMessage("Surname is empty")
                .Length(2, 20);
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .WithMessage("Name is empty")
                .Length(2, 20);
        }
    }
}
