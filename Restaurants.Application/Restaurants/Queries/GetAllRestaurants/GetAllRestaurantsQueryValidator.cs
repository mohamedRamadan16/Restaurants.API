using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedPages = [5, 10, 15, 30];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(o => o.pageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(o => o.pageSize)
            .Must(r => allowedPages.Contains(r))
            .WithMessage($"Page Size Must Be : [{string.Join(",", allowedPages)}]");

    }
}
