using System;
using System.ComponentModel.DataAnnotations;

namespace OurFoodChain.Data.Models {

    public class Era {

        public int Id { get; set; }
        public int WorldId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset EndTimestamp { get; set; }
        public DateTimeOffset StartTimestamp { get; set; }

        public virtual World World { get; set; }

    }

}