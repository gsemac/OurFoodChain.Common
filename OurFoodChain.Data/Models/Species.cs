namespace OurFoodChain.Data.Models {

    public class Species :
        TaxonBase<SpeciesCommonName> {

        public int? GenusId { get; set; }

        public virtual Clade Genus { get; set; }

    }

}