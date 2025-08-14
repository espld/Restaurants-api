using FluentValidation.TestHelper;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "testName",
                Description = "testDesc",
                Category = "testCat",
                ContactEmail = "test@test.com",
                PostalCode = "11-123"
            };

            var validator = new CreateRestaurantCommandValidator();

            //act

            var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();

        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "te",
                Description = "testDesc",
                Category = "testCat",
                ContactEmail = "@test.com",
                PostalCode = "11123"
            };

            var validator = new CreateRestaurantCommandValidator();

            //act

            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

        [Theory()]
        [InlineData("10245")]
        [InlineData("102-45")]
        [InlineData("10 245")]
        [InlineData("10- 245")]
        public void Validator_ForInvalidPostalCodes_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            //arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}