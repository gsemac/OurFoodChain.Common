namespace OurFoodChain.Data.Models {

    public class GalleryPicture {

        public int PictureId { get; set; }
        public int GalleryId { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual Gallery Gallery { get; set; }

    }

}