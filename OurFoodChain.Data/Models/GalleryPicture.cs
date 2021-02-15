namespace OurFoodChain.Models {

    public class GalleryPicture {

        public int PictureId { get; set; }
        public int GalleryId { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual PictureGallery Gallery { get; set; }

    }

}