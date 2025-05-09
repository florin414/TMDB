using PopcornHub.Domain.DTOs.Movies;

namespace PopcornHub.Web.Validators;
using FluentValidation;

public class SearchMoviesRequestValidator : AbstractValidator<SearchMoviesRequest>
{
    public SearchMoviesRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 100)
            .WithMessage("The 'Name' query parameter must be between 3 and 100 characters.");

        RuleFor(x => x.Genre)
            .Length(3, 50)
            .WithMessage("The 'Genre' query parameter must be between 3 and 50 characters.");
    }
}
