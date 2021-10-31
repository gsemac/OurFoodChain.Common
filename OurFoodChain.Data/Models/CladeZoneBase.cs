using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public abstract class CladeZoneBase {

        [Required]
        public int CladeId { get; set; }
        [Required]
        public int ZoneId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Clade Clade { get; set; }
        public virtual Zone Zone { get; set; }

    }

}