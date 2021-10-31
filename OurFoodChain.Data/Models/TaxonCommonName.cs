namespace OurFoodChain.Data.Models {

    public class TaxonCommonName :
        CommonNameBase {

        public int TaxonId { get; set; }

        public virtual Taxon Taxon { get; set; }

    }

}