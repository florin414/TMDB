
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PopcornHub.Domain.Entities;
using PopcornHub.Infrastructure.Configurations;

namespace PopcornHub.Infrastructure.Context;

public class PopcornHubDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public PopcornHubDbContext(DbContextOptions<PopcornHubDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Comment> Comments => Set<Comment>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        // CRAIOVAflorin2001@
        // florinliviu2001
        // dotnet tool run dotnet-ef migrations add Create_Comment_Table --context PopcornHubDbContext --project PopcornHub.Infrastructure --startup-project PopcornHub.Web
        // dotnet tool run dotnet-ef database update --context PopcornHubDbContext --project PopcornHub.Infrastructure --startup-project PopcornHub.Web
    }
}