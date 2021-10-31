using OurFoodChain.Roles;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Data.Models {

    public class CladeRole {

        [Required]
        public int CladeId { get; set; }
        public int? CustomRoleId { get; set; }
        [Required]
        public EcologicalRole RoleType { get; set; } = EcologicalRole.None;
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public virtual Clade Clade { get; set; }
        public virtual CustomRole CustomRole { get; set; }

    }

}