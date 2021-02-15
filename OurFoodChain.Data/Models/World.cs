﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Models {

    public class World {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    }

}