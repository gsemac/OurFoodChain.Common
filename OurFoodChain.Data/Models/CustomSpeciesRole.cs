using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Models {

    public class CustomSpeciesRole {

        public int Id { get; set; }
        public int WorldId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual World World { get; set; }

    }

}