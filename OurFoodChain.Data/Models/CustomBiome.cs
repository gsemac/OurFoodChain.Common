using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Data.Models {

    public class CustomBiome {

        public int Id { get; set; }
        public int WorldId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmojiIcon { get; set; }
        public string HexColor { get; set; }

        public virtual World World { get; set; }

    }

}