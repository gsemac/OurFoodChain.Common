using Microsoft.EntityFrameworkCore;

namespace OurFoodChain.Data.Models {

    [Keyless]
    public class SpeciesZoneEdit :
        SpeciesZoneBase {

        public int Id { get; set; }
        public EditType EditType { get; set; } = EditType.None;

    }

}