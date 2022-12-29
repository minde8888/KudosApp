using FluentValidation.TestHelper;
using Kudos.Services.Dtos;
using Kudos.Services.Validators;

namespace Tests.Validators
{
    public class KudoValidatorTests
    {
        private readonly KudoValidator _kudoValidator = new KudoValidator();

        [Fact]
        public void GivenAnInvalidKudoValue_ShouldHaveValidatorError()
        {
            var kudo = new KudoRequest();
            var result = _kudoValidator.TestValidate(kudo);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.SenderId);
            result.ShouldHaveValidationErrorFor(x => x.ReceiverId);
        }

        [Fact]
        public void Should_not_have_error_when_kudo_is_specified()
        {
            var kudo = new KudoRequest { 
                Description = "test" ,
                SenderId = 1,
                ReceiverId = 1
            };
            var result = _kudoValidator.TestValidate(kudo);
            result.ShouldNotHaveValidationErrorFor(k => k.Description);
            result.ShouldNotHaveValidationErrorFor(k => k.SenderId);
            result.ShouldNotHaveValidationErrorFor(k => k.ReceiverId);
        }
    }
}
