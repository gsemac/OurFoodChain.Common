using OurFoodChain.Taxonomy;
using System.Collections.Generic;

namespace OurFoodChain.Data.Models {

    public class Taxon :
        TaxonBase<TaxonCommonName> {

        public int? ParentId { get; set; }

        public Rank Rank { get; set; } = Rank.Unranked;
        public RankOffset Offset { get; set; } = RankOffset.None;

        public virtual IEnumerable<TaxonAncestor> Ancestors { get; set; }
        public virtual Taxon Parent { get; set; }

    }

}