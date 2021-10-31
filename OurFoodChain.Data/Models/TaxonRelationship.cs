using OurFoodChain.Relationships;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class TaxonRelationship {

        public int AgentId { get; set; }
        public int PatientId { get; set; }
        public int? CustomRelationshipId { get; set; }
        public EcologicalRelationship RelationshipType { get; set; } = EcologicalRelationship.None;
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Taxon Agent { get; set; }
        public virtual Taxon Patient { get; set; }
        public virtual CustomRelationship CustomRelationship { get; set; }

    }

}