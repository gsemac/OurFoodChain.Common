using System;

namespace OurFoodChain.Data.Models {

    // The Mother and Father flags can be used to flag ancestors of hybrid species. 

    [Flags]
    public enum AncestorFlags {
        None = 0,
        /// <summary>
        /// Used to indicate the mother species for hybrid species.
        /// </summary>
        Mother = 1,
        /// <summary>
        /// Used to indicate the father species for hybrid species.
        /// </summary>
        Father = 2,
        /// <summary>
        /// Used to exclude a branch from paraphyletic groupings.
        /// </summary>
        ExcludeBranch = 4,
    }

}