using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurantCommand;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {

        private readonly List<string> validCategories = ["Italian", "Mexican", "Egyptian", "American"];
        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(4, 100);

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Enter a valid Email Address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a postal code (XX-XXX)");

            #region Custome Validations

            RuleFor(dto => dto.Category)
                .Custom((value, context) =>
                {
                    var isValidCategory = validCategories.Contains(value);
                    if (!isValidCategory)
                        context.AddFailure("Please Enter A Valid Category");
                });

            /// This is Equals To

            //RuleFor(dto => dto.Category)
            //    .Must(category => validCategories.Contains(category)); 

            #endregion
        }
    }
}
