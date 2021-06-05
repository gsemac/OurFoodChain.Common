namespace OurFoodChain.Data.Models {

    public class ZoneField :
         FieldBase {

        public int ZoneId { get; set; }

        public virtual Zone Zone { get; set; }

    }

}