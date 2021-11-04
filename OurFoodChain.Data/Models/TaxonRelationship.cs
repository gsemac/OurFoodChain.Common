﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class TaxonRelationship {

        [Required]
        public int AgentId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int RelationshipId { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Taxon Agent { get; set; }
        public virtual Taxon Patient { get; set; }
        public virtual Relationship Relationship { get; set; }

    }

}