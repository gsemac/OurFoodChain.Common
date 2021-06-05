using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Data.Models {

    public abstract class FieldBase {

        [Required]
        public string Name { get; set; }
        public string Value { get; set; }
        public FieldType Type { get; set; } = FieldType.Auto;

    }

}