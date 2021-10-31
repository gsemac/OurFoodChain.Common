﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class CreatorFavorite {

        public int CladeId { get; set; }
        public int CreatorId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Clade Clade { get; set; }
        public virtual Creator Creator { get; set; }

    }

}