using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopcornHub.Domain.Entities;
using PopcornHub.Domain.ValueObjects;

namespace PopcornHub.Infrastructure.EntityConfigurations;

public class MovieCommentConfiguration : IEntityTypeConfiguration<MovieComment>
{
    public void Configure(EntityTypeBuilder<MovieComment> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.OwnsOne(x => x.MovieId, movieId =>
        {
            movieId.Property(m => m.Value)
                .HasColumnName(nameof(MovieId))
                .IsRequired();
        });

        builder.OwnsOne(x => x.Comment, comment =>
        {
            comment.Property(c => c.Value)
                .HasColumnName(nameof(Comment))
                .IsRequired();
        });
        
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany() 
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
