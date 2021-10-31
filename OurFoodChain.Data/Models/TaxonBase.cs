using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public abstract class TaxonBase<CommonNameT> {

        public int Id { get; set; }
        public int WorldId { get; set; }
        public int? GalleryId { get; set; }
        public int? DisplayCommonNameId { get; set; }
        public int? DisplayPictureId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual World World { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual CommonNameT DisplayCommonName { get; set; }
        public virtual IEnumerable<CommonNameT> CommonNames { get; }
        public virtual Picture DisplayPicture { get; set; }

    }

}