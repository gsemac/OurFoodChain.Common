using Microsoft.EntityFrameworkCore;
using OurFoodChain.Models;

namespace OurFoodChain.Data {

    public interface IOurFoodChainDbContext {

        DbSet<Creator> Creators { get; set; }

    }

}