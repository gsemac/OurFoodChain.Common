namespace OurFoodChain.Models {

    public class SpeciesField :
        FieldBase {

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

    }

}