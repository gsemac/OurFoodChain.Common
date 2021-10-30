namespace OurFoodChain.Data.Models {

    public class Species :
        TaxonBase<SpeciesCommonName> {

        public int? AncestorId { get; set; }
        public int? GenusId { get; set; }

        public virtual Species Ancestor { get; set; }
        public virtual Clade Genus { get; set; }

    }

}