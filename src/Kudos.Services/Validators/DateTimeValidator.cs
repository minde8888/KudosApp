using FluentValidation;

namespace Kudos.Services.Validators
{
    public class DateTimeValidator : AbstractValidator<DateTime>
    {
        public DateTimeValidator()
        {
            RuleFor(x => x).Must(BeValidDate).WithMessage("Date must be between 2022/11 and today");
        }

        protected bool BeValidDate(DateTime date)
        {
            if (date <= DateTime.Now && date >= new DateTime(2022, 11, 01))
            return true;

            return false;
        }
    }
}
