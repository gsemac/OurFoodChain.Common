using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurFoodChain.Models {

    public class Creator {

        public int Id { get; set; }
        [Required]
        public string DisplayName { get; set; }
        public ulong? DiscordUserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    }

}