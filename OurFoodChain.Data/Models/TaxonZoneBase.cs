using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public abstract class TaxonZoneBase {

        [Required]
        public int TaxonId { get; set; }
        [Required]
        public int ZoneId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Taxon Taxon { get; set; }
        public virtual Zone Zone { get; set; }

    }

}