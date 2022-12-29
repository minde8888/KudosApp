
using FluentValidation.TestHelper;
using Kudos.Services.Validators;

namespace Tests.Validators
{
    public class DateTimeValidatorTests
    {
        private readonly DateTimeValidator _dateTimeValidator = new DateTimeValidator();

        [Fact]
        public void GivenAnInvalidDateValue_ShouldHaveValidatorError()
        {
            var date = new DateTime(2021, 11, 01);
            var result = _dateTimeValidator.TestValidate(date);
            result.ShouldHaveAnyValidationError();
        }

        [Fact]
        public void Should_not_have_error_when_given_correct_DateTime()
        {
            var result = _dateTimeValidator.TestValidate(DateTime.Now);
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
        }
    }
}
