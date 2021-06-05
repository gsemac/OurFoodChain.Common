using OurFoodChain.Relationships;

namespace OurFoodChain.Data.Models {

    public class SpeciesRelationship {

        public int ActingSpeciesId { get; set; }
        public int ReceivingSpeciesId { get; set; }
        public int? CustomRelationshipId { get; set; }
        public EcologicalRelationshipId Relationship { get; set; } = EcologicalRelationshipId.None;
        public string Description { get; set; }

        public virtual Species ActingSpecies { get; set; }
        public virtual Species ReceivingSpecies { get; set; }
        public virtual CustomSpeciesRelationship CustomRelationship { get; set; }

    }

}