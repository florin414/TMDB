using PopcornHub.Domain.DTOs.Movie;
using PopcornHub.Domain.Models.Movie;

namespace PopcornHub.Web.DomainMappings.Movie;

public static class MoviesMapper
{
    public static MoviesModel ToModel(this MoviesRequest request)
        => new()
        {
            Genre = request.Genre,
            Name = request.Name,
            PageSize = request.PageSize,
            Page = request.Page,
            SortBy = request.SortBy,
        };

    public static MoviesResponse ToResponse(this GetMoviesModel model)
        => new()
        {
            Movies = model.Movies,
            TotalCount = model.TotalCount,
            PageSize = model.PageSize,
            Page = model.Page,
        };
}