using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Models {

    public abstract class FieldBase {

        [Required]
        public string Name { get; set; }
        public string Value { get; set; }
        public FieldType Type { get; set; } = FieldType.Auto;

    }

}