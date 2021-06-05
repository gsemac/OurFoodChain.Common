using OurFoodChain.Taxonomy;

namespace OurFoodChain.Data.Models {

    public class Clade :
        TaxonBase<CladeCommonName> {

        public int? ParentId { get; set; }

        public TaxonRankId Rank { get; set; } = TaxonRankId.Unranked;
        public TaxonPosition Position { get; set; } = TaxonPosition.On;

        public virtual Clade Parent { get; set; }

    }

}