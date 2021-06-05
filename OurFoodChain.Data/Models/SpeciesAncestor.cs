namespace OurFoodChain.Data.Models {

    public class SpeciesAncestor {

        public int SpeciesId { get; set; }
        public int AncestorId { get; set; }

        public Species Species { get; set; }
        public Species Ancestor { get; set; }

    }

}