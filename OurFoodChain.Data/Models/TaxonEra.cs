using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Data.Models {

    public class TaxonEra {

        [Required]
        public int TaxonId { get; set; }
        [Required]
        public int EraId { get; set; }

        public virtual Taxon Taxon { get; set; }
        public virtual Era Era { get; set; }

    }

}