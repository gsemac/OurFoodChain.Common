using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    public class OurFoodChainIdentityDbContext :
        IdentityDbContext<IdentityUser>,
        IOurFoodChainDbContext {

        public DbSet<Creator> Creators { get; set; }

        public OurFoodChainIdentityDbContext(DbContextOptions<OurFoodChainIdentityDbContext> options) :
            base(options) {
        }
        public OurFoodChainIdentityDbContext(DbContextOptions options) :
            base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            OurFoodChainDbContextUtilities.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

    }

}