using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Ratings.Data.StarRatings
{
    public class StarRatingContext : DbContext
    {
        public StarRatingContext(DbContextOptions<StarRatingContext> options)
            : base(options)
        {
        }        

        public DbSet<StarRatingData> StarRatings { get; set; }
    }
}