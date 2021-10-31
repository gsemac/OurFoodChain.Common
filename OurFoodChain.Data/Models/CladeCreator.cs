namespace OurFoodChain.Data.Models {

    public class CladeCreator {

        public int CladeId { get; set; }
        public int CreatorId { get; set; }

        public CreatorRoles Roles { get; set; } = CreatorRoles.None;

        public virtual Clade Clade { get; set; }
        public virtual Creator Creator { get; set; }

    }

}