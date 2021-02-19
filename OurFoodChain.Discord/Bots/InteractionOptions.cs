using System;

namespace OurFoodChain.Discord.Bots {

    public class InteractionOptions :
        IInteractionOptions {

        public bool AllowCancellation { get; set; } = true;
        public bool RequireSourceUser { get; set; } = true;
        public bool RequireSourceChannel { get; set; } = true;
        public TimeSpan? Timeout { get; set; }

        public static InteractionOptions Default => new InteractionOptions();

    }

}