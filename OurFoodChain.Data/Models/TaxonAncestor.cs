using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class TaxonAncestor {

        [Required]
        public int AncestorId { get; set; }
        [Required]
        public int TaxonId { get; set; }

        public AncestorFlags Flags { get; set; } = AncestorFlags.None;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Taxon Ancestor { get; set; }
        public virtual Taxon Taxon { get; set; }

    }

}