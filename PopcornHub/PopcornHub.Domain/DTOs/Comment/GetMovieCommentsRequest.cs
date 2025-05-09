using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Comment;

public class GetMovieCommentsRequest : EntityDto
{
    public int MovieId { get; set; }
}