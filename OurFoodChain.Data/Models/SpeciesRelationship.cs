using OurFoodChain.Relationships;

namespace OurFoodChain.Data.Models {

    public class SpeciesRelationship {

        public int ObjectSpeciesId { get; set; }
        public int SubjectSpeciesId { get; set; }
        public int? CustomRelationshipId { get; set; }
        public EcologicalRelationship Relationship { get; set; } = EcologicalRelationship.None;
        public string Description { get; set; }

        public virtual Species ActingSpecies { get; set; }
        public virtual Species ReceivingSpecies { get; set; }
        public virtual CustomSpeciesRelationship CustomRelationship { get; set; }

    }

}