using OurFoodChain.Roles;

namespace OurFoodChain.Models {

    public class SpeciesRole {

        public int SpeciesId { get; set; }
        public int? CustomRoleId { get; set; }
        public EcologicalRoleId Role { get; set; } = EcologicalRoleId.None;
        public string Description { get; set; }

        public virtual Species Species { get; set; }
        public virtual CustomSpeciesRole CustomRole { get; set; }

    }

}