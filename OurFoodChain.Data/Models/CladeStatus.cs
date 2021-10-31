using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class CladeStatus {

        [Key, Required]
        public int CladeId { get; set; }

        [Required]
        public ConservationStatus Status { get; set; } = ConservationStatus.NotEvaluated;
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Clade Clade { get; set; }

    }

}