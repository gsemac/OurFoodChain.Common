namespace OurFoodChain.Data.Models {

    public class CladeField :
        FieldBase {

        public int CladeId { get; set; }

        public virtual Clade Clade { get; set; }

    }

}