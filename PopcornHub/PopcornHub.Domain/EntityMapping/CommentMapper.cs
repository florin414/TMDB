using FastEndpoints;
using PopcornHub.Domain.DTOs.Comment;
using PopcornHub.Domain.Entities;

namespace PopcornHub.Domain.DomainEntityMapping;

public class CommentMapper : Mapper<CreateMovieCommentRequest, CreateMovieCommentResponse, Comment>
{
    public override Comment ToEntity(CreateMovieCommentRequest rEquest) => 
        new Comment(rEquest.UserId, rEquest.MovieId, rEquest.Text);
    
    public override CreateMovieCommentResponse FromEntity(Comment entity) =>
        new CreateMovieCommentResponse(entity.Id, entity.MovieId, entity.Text);
}

// move
// -- domain mapping and entity mapping 