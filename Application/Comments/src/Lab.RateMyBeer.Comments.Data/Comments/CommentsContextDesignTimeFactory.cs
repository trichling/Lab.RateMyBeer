using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab.RateMyBeer.Comments.Data.Comments;

public class CommentsContextDesignFactory : IDesignTimeDbContextFactory<CommentsContext>
{
    public CommentsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CommentsContext>();
        optionsBuilder.UseSqlServer(
            "Server=(local);Database=CommentsDb;User Id=sa;Password=1stChangeIt!;MultipleActiveResultSets=true");

        return new CommentsContext(optionsBuilder.Options);
    }
}