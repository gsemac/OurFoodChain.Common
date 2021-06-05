namespace OurFoodChain.Data.Models {

    public class ZoneCommonName :
        CommonNameBase {

        public int ZoneId { get; set; }

        public virtual Zone Zone { get; set; }

    }

}