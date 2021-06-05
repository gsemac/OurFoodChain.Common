namespace OurFoodChain.Data.Models {

    public class SpeciesCreator {

        public int SpeciesId { get; set; }
        public int CreatorId { get; set; }

        public virtual Species Species { get; set; }
        public virtual Creator Creator { get; set; }

    }

}