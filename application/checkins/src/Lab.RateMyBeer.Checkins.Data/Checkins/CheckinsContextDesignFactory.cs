using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab.RateMyBeer.Checkins.Data.Checkins;
public class CheckinsContextDesignFactory : IDesignTimeDbContextFactory<CheckinsContext>
{
    public CheckinsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CheckinsContext>();
        optionsBuilder.UseSqlServer(
            "Server=(local);Database=CheckinsDb;User Id=sa;Password=1stChangeIt!;MultipleActiveResultSets=true");

        return new CheckinsContext(optionsBuilder.Options);
    }
}
