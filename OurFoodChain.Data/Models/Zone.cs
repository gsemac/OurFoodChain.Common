using OurFoodChain.Zones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class Zone {

        [Required, Key]
        public int Id { get; set; }
        [Required]
        public int WorldId { get; set; }
        public int? BiomeId { get; set; }
        public int? ParentId { get; set; }
        public int? GalleryId { get; set; }
        public int? DisplayedCommonNameId { get; set; }
        public int? DisplayedPictureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ZoneFlags Flags { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }
        public virtual Biome Biome { get; set; }
        public virtual Zone Parent { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ZoneCommonName DisplayedCommonName { get; set; }
        public virtual IEnumerable<ZoneCommonName> CommonNames { get; }

    }

}