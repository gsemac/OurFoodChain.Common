namespace OurFoodChain.Models {

    public class CladeCommonName :
        CommonNameBase {

        public int CladeId { get; set; }

        public virtual Clade Clade { get; set; }

    }

}