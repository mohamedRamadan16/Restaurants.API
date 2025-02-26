using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private int[] allowedPages = [5, 10, 15, 30];
    private string[] sortParameters = [nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(o => o.pageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(o => o.pageSize)
            .Must(r => allowedPages.Contains(r))
            .WithMessage($"Page Size Must Be : [{string.Join(",", allowedPages)}]");

        RuleFor(o => o.SortBy)
            .Must(value => sortParameters.Contains(value))
            .When(p => p.SortBy != null)
            .WithMessage($"Sort By is optional and it must be within : [{string.Join(", ", sortParameters)}]");
    }
}
