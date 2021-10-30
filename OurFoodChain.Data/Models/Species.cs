using System;

namespace OurFoodChain.Data.Models {

    public class Species {

        public int Id { get; set; }
        public int? AncestorId { get; set; }
        public int CladeId { get; set; }

        public ConservationStatus Status { get; set; } = ConservationStatus.LeastConcern;
        public string StatusDescription { get; set; }
        public DateTimeOffset? StatusTimestamp { get; set; }

        public virtual Species Ancestor { get; set; }
        public virtual Clade Clade { get; set; }

    }

}