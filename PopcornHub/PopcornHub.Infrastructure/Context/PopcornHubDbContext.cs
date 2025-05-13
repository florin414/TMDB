using Microsoft.EntityFrameworkCore;
using PopcornHub.Domain.Entities;
using PopcornHub.Infrastructure.EntityConfigurations;

namespace PopcornHub.Infrastructure.Context;

public class PopcornHubDbContext : DbContext
{
    public PopcornHubDbContext(DbContextOptions<PopcornHubDbContext> options) : base(options) { }

    public virtual DbSet<MovieComment> Comments => Set<MovieComment>();
    
    public virtual DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new MovieCommentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        // CRAIOVAflorin2001@
        // florinliviu2001
        // dotnet tool run dotnet-ef migrations add Create_Comment_Table --context PopcornHubDbContext --project PopcornHub.Infrastructure --startup-project PopcornHub.Web
        // dotnet tool run dotnet-ef database update --context PopcornHubDbContext --project PopcornHub.Infrastructure --startup-project PopcornHub.Web
    }
}