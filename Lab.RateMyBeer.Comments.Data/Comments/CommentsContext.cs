using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Comments.Data.Comments;

public class CommentsContext : DbContext
{
    public CommentsContext(DbContextOptions<CommentsContext> options)
        : base(options)
    {
        
    }

    public DbSet<CommentsData> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentsData>().HasKey(cs => cs.CommentsId);
        modelBuilder.Entity<CommentsData>()
            .HasMany(cs => cs.Comments)
            .WithOne();

        modelBuilder.Entity<CommentData>().HasKey(c => c.CommentId);
        modelBuilder.Entity<CommentData>().ToTable("CommentsCommentEntries");

    }
    
}