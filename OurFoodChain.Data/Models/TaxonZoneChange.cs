using Microsoft.EntityFrameworkCore;

namespace OurFoodChain.Data.Models {

    [Keyless]
    public class TaxonZoneChange :
        TaxonZoneBase {

        public int Id { get; set; }
        public EditType EditType { get; set; } = EditType.None;

    }

}