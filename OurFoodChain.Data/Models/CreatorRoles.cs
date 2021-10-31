using System;

namespace OurFoodChain.Data.Models {

    [Flags]
    public enum CreatorRoles {
        None = 0,
        Creator = 1,
        Submitter = 2,
        Artist = 4,
    }

}