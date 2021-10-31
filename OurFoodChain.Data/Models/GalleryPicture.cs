using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class GalleryPicture {

        [Required]
        public int PictureId { get; set; }
        [Required]
        public int GalleryId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Picture Picture { get; set; }
        public virtual Gallery Gallery { get; set; }

    }

}