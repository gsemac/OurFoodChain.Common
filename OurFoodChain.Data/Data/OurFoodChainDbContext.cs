using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    public class OurFoodChainDbContext :
        DbContext,
        IOurFoodChainDbContext {

        public DbSet<Creator> Creators { get; set; }

        public OurFoodChainDbContext(DbContextOptions<OurFoodChainDbContext> options) :
            base(options) {
        }
        public OurFoodChainDbContext(DbContextOptions options) :
            base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            OurFoodChainDbContextUtilities.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

    }

}