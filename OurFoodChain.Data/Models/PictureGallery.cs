using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Models {

    public class PictureGallery {

        public int Id { get; set; }
        public int WorldId { get; set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }
        public virtual IEnumerable<Picture> Pictures { get; }

    }

}