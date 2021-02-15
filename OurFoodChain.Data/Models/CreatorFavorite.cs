namespace OurFoodChain.Models {

    public class CreatorFavorite {

        public int CreatorId { get; set; }
        public int SpeciesId { get; set; }

        public virtual Creator Creator { get; set; }
        public virtual Species Species { get; set; }

    }

}