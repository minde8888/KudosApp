using FluentValidation;
using Kudos.Services.Dtos;

namespace Kudos.Services.Validators
{
    public class KudoValidator : AbstractValidator<KudoRequest>
    {
        public KudoValidator()
        {
            RuleFor(x => x.Description).NotNull().Length(2, 200);
            RuleFor(x => x.SenderId).NotNull().WithMessage("Number of id can't be empty.")
                .GreaterThan(0).WithMessage("Number of id must be greater than 0.");
            RuleFor(x => x.ReceiverId).NotNull().WithMessage("Number of id can't be empty.")
                .GreaterThan(0).WithMessage("Number of id must be greater than 0.");
        }
    }
}
