using FluentValidation.TestHelper;
using Kudos.Services.Dtos;
using Kudos.Services.Validators;

namespace Tests.Validators
{
    public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator _employeeValidator = new EmployeeValidator();

        [Fact]
        public void GivenAnInvalidEmployeeValue_ShouldHaveValidatorError()
        {
            var employee = new EmployeeRequest();
            var result = _employeeValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }

        [Fact]
        public void Should_not_have_error_when_employee_is_specified()
        {
            var employee = new EmployeeRequest
            {
                Name = "name",
                Surname = "surname"
            };
            var result = _employeeValidator.TestValidate(employee);
            result.ShouldNotHaveValidationErrorFor(k => k.Name);
            result.ShouldNotHaveValidationErrorFor(k => k.Surname);
        }
    }
}
