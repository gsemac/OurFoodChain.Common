using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class Biome {

        [Required, Key]
        public int Id { get; set; }
        [Required]
        public int WorldId { get; set; }
        [Required]
        public string Name { get; set; }
        public string EmojiIcon { get; set; }
        public string HexColor { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }

    }

}