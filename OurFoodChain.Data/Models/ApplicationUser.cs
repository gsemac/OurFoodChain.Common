using Microsoft.AspNetCore.Identity;

namespace OurFoodChain.Data.Models {

    public class ApplicationUser :
        IdentityUser {

        public int CreatorId { get; set; }

        public virtual Creator Creator { get; set; }

    }

}