using OurFoodChain.Taxonomy;
using System.Collections.Generic;

namespace OurFoodChain.Data.Models {

    public class Clade :
        TaxonBase<CladeCommonName> {

        public int? ParentId { get; set; }

        public Rank Rank { get; set; } = Rank.Unranked;
        public RankPosition Position { get; set; } = RankPosition.On;

        public virtual IEnumerable<CladeAncestor> Ancestors { get; set; }
        public virtual Clade Parent { get; set; }

    }

}