namespace OurFoodChain.Models {

    public class CreatorWorldPermissions {

        public int WorldId { get; set; }
        public int CreatorId { get; set; }
        public WorldPermissions Permissions { get; set; }

        public virtual World World { get; set; }
        public virtual Creator Creator { get; set; }

    }

}