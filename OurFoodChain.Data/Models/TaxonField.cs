using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Data.Models {

    public class TaxonField :
        FieldBase {

        [Required]
        public int TaxonId { get; set; }

        public virtual Taxon Taxon { get; set; }

    }

}