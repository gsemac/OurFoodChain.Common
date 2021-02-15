using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Models {

    public abstract class TaxonBase<CommonNameT> {

        public int Id { get; set; }
        public int WorldId { get; set; }
        public int? GalleryId { get; set; }
        public int? DisplayedCommonNameId { get; set; }
        public int? DisplayedPictureId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }
        public virtual PictureGallery Gallery { get; set; }
        public virtual CommonNameT DisplayedCommonName { get; set; }
        public virtual IEnumerable<CommonNameT> CommonNames { get; }
        public virtual Picture DisplayedPicture { get; set; }

    }

}