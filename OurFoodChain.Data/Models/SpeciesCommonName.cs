namespace OurFoodChain.Models {

    public class SpeciesCommonName :
         CommonNameBase {

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

    }

}