using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Models {

    public class Creator {

        public int Id { get; set; }
        [Required]
        public string DisplayName { get; set; }

    }

}