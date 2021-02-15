using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Models {

    public abstract class SpeciesZoneBase {

        public int SpeciesId { get; set; }
        public int ZoneId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Species Species { get; set; }
        public virtual Zone Zone { get; set; }

    }

}