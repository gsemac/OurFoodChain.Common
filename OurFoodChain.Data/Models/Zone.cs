using OurFoodChain.Zones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class Zone {

        public int Id { get; set; }
        public int WorldId { get; set; }
        public int? CustomBiomeId { get; set; }
        public int? ParentId { get; set; }
        public int? GalleryId { get; set; }
        public int? DisplayedCommonNameId { get; set; }
        public int? DisplayedPictureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Biome BiomeType { get; set; } = Biome.None;
        public ZoneFlags Flags { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }
        public virtual CustomBiome CustomZoneType { get; set; }
        public virtual Zone Parent { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ZoneCommonName DisplayedCommonName { get; set; }
        public virtual IEnumerable<ZoneCommonName> CommonNames { get; }

    }

}