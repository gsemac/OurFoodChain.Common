using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    internal static class OurFoodChainDbContextUtilities {

        public static void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Creator>().ToTable("Creators");

        }

    }

}