using Volo.Abp.Application.Dtos;

namespace PopcornHub.Domain.DTOs.Comment;

public class GetMovieCommentsResponse : EntityDto
{
    public List<Entities.Comment> Comments { get; set; }
}