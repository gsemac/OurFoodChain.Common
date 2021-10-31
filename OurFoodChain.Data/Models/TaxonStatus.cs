using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class TaxonStatus {

        [Key, Required]
        public int TaxonId { get; set; }

        [Required]
        public ConservationStatus Status { get; set; } = ConservationStatus.NotEvaluated;
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Taxon Taxon { get; set; }

    }

}