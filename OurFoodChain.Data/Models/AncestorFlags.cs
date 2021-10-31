using System;

namespace OurFoodChain.Data.Models {

    // The Mother and Father flags can be used to flag ancestors of hybrid species. 

    [Flags]
    public enum AncestorFlags {
        None = 0,
        Mother = 1,
        Father = 2,
    }

}