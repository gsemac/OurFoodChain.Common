using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class CladeAncestor {

        [Required]
        public int AncestorId { get; set; }
        [Required]
        public int CladeId { get; set; }

        public AncestorFlags Flags { get; set; } = AncestorFlags.None;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Clade Ancestor { get; set; }
        public virtual Clade Clade { get; set; }

    }

}