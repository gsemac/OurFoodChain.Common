namespace OurFoodChain.Data.Models {

    public class TaxonCreator {

        public int TaxonId { get; set; }
        public int CreatorId { get; set; }

        public CreatorRoles Roles { get; set; } = CreatorRoles.None;

        public virtual Taxon Taxon { get; set; }
        public virtual Creator Creator { get; set; }

    }

}