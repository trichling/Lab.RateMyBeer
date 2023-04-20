using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab.RateMyBeer.Ratings.Data.StarRatings
{
    public class StarRatingContextDesignFactory : IDesignTimeDbContextFactory<StarRatingContext>
    {
        public StarRatingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StarRatingContext>();
            optionsBuilder.UseSqlServer(
                "Server=(local);Database=CheckinsDb;User Id=sa;Password=1stChangeIt!;MultipleActiveResultSets=true");

            return new StarRatingContext(optionsBuilder.Options);
        }
    }
}
