using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Checkins.Data.Checkins
{
    public class CheckinsContext : DbContext
    {

        public CheckinsContext(DbContextOptions<CheckinsContext> options)
            : base(options)
        {
            
        }

        public DbSet<CheckinData> Checkins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckinData>().HasKey(d => d.CheckinId);
        }
    }
}
